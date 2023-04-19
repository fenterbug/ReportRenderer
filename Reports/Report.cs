using System.Data;

namespace Sandbox
{
	public class Report : ReportGroup
	{
		private readonly DataTable _table;

		public Report(DataTable table, IReportRenderer renderer) : base(null, renderer) => _table = table;

		public void Render() => Render(_table.AsEnumerable().ToList());
	}
}