using Sandbox;

using System.Data;

namespace Reports.Sample
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			PlayWithReports();
		}

		private static void PlayWithReports()
		{
			DataTable dt = RetrieveData();
			IReportRenderer reportRenderer = new ConsoleReportRenderer();

			var report = new Report(dt, reportRenderer);

			#region [ If we do nothing... ]

			//var defaultDetailFormat = new DisplayFormat(0, 0, 1, false);

			//report.AddDetailComponent("Detail:", defaultDetailFormat.Copy().AtColumn(ColumnPosition.LabelColumn));
			//report.AddDetailComponent(dt.Columns[DataTableColumns.FirstColumnName], defaultDetailFormat.Copy().AtColumn(ColumnPosition.ManagesOrdersColumn));
			//report.AddDetailComponent(dt.Columns[DataTableColumns.SecondColumnName], defaultDetailFormat.Copy().AtColumn(ColumnPosition.ParentIDColumn));
			//report.AddDetailComponent(dt.Columns[DataTableColumns.ThirdColumnName], defaultDetailFormat.Copy().AtColumn(ColumnPosition.DescriptionColumn));

			//report.AddDetailComponent((data) =>
			//{
			//	var value = (bool)data[DataTableColumns.FirstColumnName] ? (int?)data[DataTableColumns.SecondColumnName] * 2 : (int?)data[DataTableColumns.SecondColumnName] / 2;
			//	return value.ToString();
			//}, defaultDetailFormat.Copy().AtColumn(ColumnPosition.CalculatedFieldColumn));

			#region [ With Headers ]

			////var defaultHeadingFormat = new DisplayFormat(0, 0, 2, true);
			////var defaultSummaryFormat = new DisplayFormat(0, 0, 1, true);

			////report.AddHeadingComponent("Report Title", defaultHeadingFormat.Copy());
			////report.AddHeadingComponent(DataTableColumns.FirstColumnName, defaultHeadingFormat.Copy().OnRow(2).AtColumn(ColumnPosition.ManagesOrdersColumn));
			////report.AddHeadingComponent(DataTableColumns.SecondColumnName, defaultHeadingFormat.Copy().OnRow(2).AtColumn(ColumnPosition.ParentIDColumn));
			////report.AddHeadingComponent(DataTableColumns.ThirdColumnName, defaultHeadingFormat.Copy().OnRow(2).AtColumn(ColumnPosition.DescriptionColumn));
			////report.AddHeadingComponent("Calculated Field", defaultHeadingFormat.Copy().OnRow(2).AtColumn(ColumnPosition.CalculatedFieldColumn));

			////var reportSummaryText = "Grand Total: ";
			////report.AddSummaryComponent(reportSummaryText, defaultSummaryFormat.Copy().AtColumn(ColumnPosition.CalculatedFieldColumn - reportSummaryText.Length));
			////report.AddSummaryComponent((data) => data.Sum(row => (int?)row[DataTableColumns.SecondColumnName]).ToString(),
			////											defaultSummaryFormat.Copy().AtColumn(ColumnPosition.CalculatedFieldColumn));

			#region [ Grouping ]

			//////var firstGroup = report.AddGroup(DataTableColumns.FirstColumnName);

			//////firstGroup.AddHeadingComponent("Heading for: ", defaultHeadingFormat.Copy().AtColumn(firstGroup.NestingLevel * 3));
			//////firstGroup.AddHeadingComponent(dt.Columns[DataTableColumns.FirstColumnName], defaultHeadingFormat.Copy().OnRow(1).AtColumn(firstGroup.NestingLevel * 3));

			//////firstGroup.AddHeadingComponent("Sum: ", defaultHeadingFormat.Copy().AtColumn(ColumnPosition.CalculatedFieldColumn));
			//////firstGroup.AddHeadingComponent((data) => data.Sum(row => (int?)row[DataTableColumns.SecondColumnName]).ToString(),
			//////											defaultSummaryFormat.Copy().OnRow(1).AtColumn(ColumnPosition.CalculatedFieldColumn));

			//////var firstGroupSummaryText = "Group 1 Total: ";
			//////firstGroup.AddSummaryComponent(firstGroupSummaryText, defaultSummaryFormat.Copy().AtColumn(ColumnPosition.CalculatedFieldColumn - firstGroupSummaryText.Length));
			//////firstGroup.AddSummaryComponent((data) => data.Sum(row => (int?)row[DataTableColumns.SecondColumnName]).ToString(),
			//////											defaultSummaryFormat.Copy().AtColumn(ColumnPosition.CalculatedFieldColumn));

			#region [ More Grouping ]

			////////var secondGroup = firstGroup.AddGroup(new[] { DataTableColumns.SecondColumnName });
			////////secondGroup.AddHeadingComponent("Heading for: ", defaultHeadingFormat.Copy().AtColumn(secondGroup.NestingLevel * 3));
			////////secondGroup.AddHeadingComponent(dt.Columns[DataTableColumns.SecondColumnName], defaultHeadingFormat.Copy().AtColumn(20));

			////////secondGroup.AddSummaryComponent("Total: ", defaultSummaryFormat.Copy().AtColumn(secondGroup.NestingLevel * 3).OnRow(1));
			////////secondGroup.AddSummaryComponent((data) => data.Sum(row => (int?)row[DataTableColumns.SecondColumnName]).ToString(),
			////////											defaultSummaryFormat.Copy().OnRow(1).AtColumn(20));

			////////secondGroup.AddSummaryComponent("Summary for ", defaultSummaryFormat.Copy().AtColumn(secondGroup.NestingLevel * 3));
			////////secondGroup.AddSummaryComponent(dt.Columns[DataTableColumns.SecondColumnName], defaultSummaryFormat.Copy().AtColumn(20));

			#endregion [ More Grouping ]

			#endregion [ Grouping ]

			#endregion [ With Headers ]

			#endregion [ If we do nothing... ]

			report.Render();
		}

		private static DataTable RetrieveData()
		{
			var dt = new DataTable();

			#region [ Setup and populate ]

			dt.Columns.Add(DataTableColumns.FirstColumnName, typeof(bool));
			dt.Columns.Add(DataTableColumns.SecondColumnName, typeof(int));
			dt.Columns.Add(DataTableColumns.ThirdColumnName, typeof(string));

			dt.Rows.Add(false, 19, "IV Room");
			dt.Rows.Add(false, 19, "EMS");
			dt.Rows.Add(true, 52, "Carousel");
			dt.Rows.Add(false, 55, "IV Storage");
			dt.Rows.Add(false, 55, "IV Fridge");
			dt.Rows.Add(false, 58, "EMS Area");
			dt.Rows.Add(true, 19, "Main Pharmacy");
			dt.Rows.Add(false, 58, "Floor Stock");
			dt.Rows.Add(true, 19, "Inventory");

			#endregion [ Setup and populate ]

			return dt;
		}

		private static class ColumnPosition
		{
			public const int LabelColumn = 0;
			public const int ManagesOrdersColumn = LabelColumn + 10;
			public const int ParentIDColumn = ManagesOrdersColumn + 18;
			public const int DescriptionColumn = ParentIDColumn + 15;
			public const int CalculatedFieldColumn = DescriptionColumn + 15;
		}

		private static class DataTableColumns
		{
			public const string FirstColumnName = "ManagesOrders";
			public const string SecondColumnName = "ParentAreaID";
			public const string ThirdColumnName = "Description";
		}
	}
}