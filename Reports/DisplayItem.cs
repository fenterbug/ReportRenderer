using System.Data;

namespace Sandbox
{
	public class DisplayItem
	{
		public readonly DisplayFormat DisplayFormat;
		private readonly Func<DataRow, string>? getDetailValue;
		private readonly Func<IEnumerable<DataRow>, string>? getSummaryValue;

		private DisplayItem(DisplayFormat displayFormat)
		{
			this.DisplayFormat = displayFormat ?? throw new ArgumentNullException(nameof(displayFormat));
		}

		public DisplayItem(Func<IEnumerable<DataRow>, string> toPerform, DisplayFormat displayFormat) : this(displayFormat)
		{
			this.getSummaryValue = toPerform;
		}

		public DisplayItem(Func<DataRow, string> toPerform, DisplayFormat displayFormat) : this(displayFormat)
		{
			getDetailValue = toPerform;
		}

		public string GetText(IEnumerable<DataRow> data) => getSummaryValue(data);

		public string GetText(DataRow row) => getDetailValue(row);
	}
}