using System.Data;

namespace Sandbox
{
    public class ReportGroup
    {
        protected bool IncludeInNestingLevel = true;
        protected ReportGroup? subGroup;
        private readonly IEnumerable<DataColumn> columnsToGroupBy = new List<DataColumn>();
        private readonly List<DisplayItem> detailData = new();
        private readonly List<DisplayItem> headingData = new();
        private readonly ReportGroup? parentGroup;
        private readonly IReportRenderer renderer;
        private readonly List<DisplayItem> summaryData = new();

        protected ReportGroup(ReportGroup? _parentGroup, IReportRenderer renderer)
        {
            parentGroup = _parentGroup;
            this.renderer = renderer;
        }

        protected ReportGroup(IEnumerable<DataColumn> columnsToGroupBy, ReportGroup _parentGroup, IReportRenderer renderer) : this(_parentGroup, renderer)
        {
            this.columnsToGroupBy = this.columnsToGroupBy.Concat(columnsToGroupBy);
        }

        public int NestingLevel
        {
            get { if (parentGroup is null) return 0; else return parentGroup.NestingLevel + (IncludeInNestingLevel ? 1 : 0); }
        }

        #region [ AddDetailComponent ]

        public void AddDetailComponent(string text, DisplayFormat displayFormat)
        {
            AddDetailComponent((data) => text, displayFormat);
        }

        public void AddDetailComponent(DataColumn dataColumn, DisplayFormat displayFormat)
        {
            AddDetailComponent((data) => data[dataColumn.ColumnName].ToString(), displayFormat);
        }

        public void AddDetailComponent(Func<DataRow, string> toPerform, DisplayFormat displayFormat)
        {
            if (parentGroup is not null)
            {
                parentGroup.AddDetailComponent(toPerform, displayFormat);
            }
            else
            {
                detailData.Add(new DisplayItem(toPerform, displayFormat));
            }
        }

        #endregion [ AddDetailComponent ]

        #region [ AddGroup ]

        public ReportGroup AddGroup(DataColumn groupByColumn)
        {
            var newGroupSet = columnsToGroupBy.ToList();
            newGroupSet.Add(groupByColumn);
            subGroup = new ReportGroup(newGroupSet, this, renderer);

            return subGroup;
        }

        public ReportGroup AddGroup(string columnName)
        {
            return AddGroup(new DataColumn(columnName));
        }

        public virtual ReportGroup AddGroup(IEnumerable<DataColumn> groupByColumns)
        {
            // We have to create the first group to get a valid reference to it.
            ReportGroup newGroup = AddGroup(groupByColumns.First());

            // And since we've created that first group, remove that column from the list...
            var newSet = groupByColumns.ToList();
            newSet.RemoveAt(0);

            // ... so that we can create subgroups for each of the other remaining columns.
            if (newSet.Any())
            {
                foreach (var column in newSet)
                {
                    newGroup = newGroup.AddGroup(column);
                    newGroup.IncludeInNestingLevel = false;
                }
            }

            return newGroup;
        }

        public ReportGroup AddGroup(IEnumerable<string> groupbyColumns)
        {
            return AddGroup(groupbyColumns.Select(c => new DataColumn(c)).ToList());
        }

        #endregion [ AddGroup ]

        #region [ AddHeadingComponent ]

        public void AddHeadingComponent(string text, DisplayFormat displayFormat)
        {
            AddHeadingComponent((data) => text, displayFormat);
        }

        public void AddHeadingComponent(DataColumn dataColumn, DisplayFormat displayFormat)
        {
            AddHeadingComponent((data) => data.First()[dataColumn.ColumnName].ToString(), displayFormat);
        }

        public void AddHeadingComponent(Func<IEnumerable<DataRow>, string> toPerform, DisplayFormat displayFormat)
        {
            headingData.Add(new DisplayItem(toPerform, displayFormat));
        }

        #endregion [ AddHeadingComponent ]

        #region [ AddSummaryComponent ]

        public void AddSummaryComponent(string text, DisplayFormat displayFormat)
        {
            AddSummaryComponent((data) => text, displayFormat);
        }

        public void AddSummaryComponent(DataColumn dataColumn, DisplayFormat displayFormat)
        {
            AddSummaryComponent((data) => data.Last()[dataColumn.ColumnName].ToString(), displayFormat);
        }

        public void AddSummaryComponent(Func<IEnumerable<DataRow>, string> toPerform, DisplayFormat displayFormat)
        {
            summaryData.Add(new DisplayItem(toPerform, displayFormat));
        }

        #endregion [ AddDetailComponent ]

        #region [ Rendering ]

        protected void Render(List<DataRow> data)
        {
            if (columnsToGroupBy.Any())
            {
                var _data = data.GroupBy(row => row[columnsToGroupBy.Last().ColumnName]).Select(group => new { GroupID = group.Key, Data = group.ToList() }).ToList();

                foreach (var group in _data)
                {
                    RenderHeading(group.Data);

                    if (subGroup is not null) { subGroup.Render(group.Data); }
                    else { DoRenderDetail(group.Data); }

                    RenderSummary(group.Data);
                }
            }
            else
            {
                RenderHeading(data);

                if (subGroup is not null) { subGroup.Render(data); }
                else { DoRenderDetail(data); }

                RenderSummary(data);
            }
        }

        protected virtual void RenderDetail(DataRow data)
        {
            renderer.RenderItems(detailData, data);
        }

        protected virtual void RenderHeading(IEnumerable<DataRow> data)
        {
            if (data.Any()) renderer.RenderItems(headingData, data);
        }

        protected virtual void RenderSummary(IEnumerable<DataRow> data)
        {
            if (data.Any()) renderer.RenderItems(summaryData, data);
        }

        private void DoRenderDetail(List<DataRow> data)
        {
            if (data.Any())
            {
                if (parentGroup is not null) { parentGroup.DoRenderDetail(data); }
                else
                {
                    foreach (DataRow dataRow in data)
                    {
                        RenderDetail(dataRow);
                    }
                }
            }
        }

        #endregion [ Rendering ]
    }
}