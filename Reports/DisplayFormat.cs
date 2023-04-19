namespace Sandbox
{
	public class DisplayFormat
	{
		public int Row = 0;
		public int Column = 0;
		public int FontSize = 1;
		public bool Bold = false;
		//public font Font = "Ariel";

		public DisplayFormat()
		{ }

		public DisplayFormat(int row, int column, int fontSize, bool bold)
		{
			Row = row;
			Column = column;
			FontSize = fontSize;
			Bold = bold;
		}

		public DisplayFormat Copy()
		{
			return new DisplayFormat(Row, Column, FontSize, Bold);
		}

		public DisplayFormat OnRow(int row)
		{
			Row = row;
			return this;
		}

		public DisplayFormat AtColumn(int column)
		{
			Column = column;
			return this;
		}

		public DisplayFormat SetBolded(bool bold)
		{
			Bold = bold;
			return this;
		}

		public DisplayFormat UsingFontSize(int fontSize)
		{
			FontSize = fontSize;
			return this;
		}
	}
}