using System.Data;

namespace Sandbox
{
	public interface IReportRenderer
	{
		void RenderItems(IEnumerable<DisplayItem> items, IEnumerable<DataRow> data);

		void RenderItems(IEnumerable<DisplayItem> items, DataRow data);
	}
}