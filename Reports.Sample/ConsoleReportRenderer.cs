using Sandbox;

using System.Data;

namespace Reports.Sample
{
	public class ConsoleReportRenderer : IReportRenderer
	{
		public void RenderItems(IEnumerable<DisplayItem> items, IEnumerable<DataRow> data)
		{
			if (items.Any() && data.Any())
			{
				var currentPosition = Console.GetCursorPosition();
				var currentBackColor = Console.BackgroundColor;
				var currentForeColor = Console.ForegroundColor;

				int maxRow = 0;
				foreach (var toPrint in items)
				{
					SetCursorPosition(toPrint.DisplayFormat.Column, currentPosition.Top + toPrint.DisplayFormat.Row);
					if (toPrint.DisplayFormat.Row > maxRow) { maxRow = toPrint.DisplayFormat.Row; }

					if (toPrint.DisplayFormat.Bold)
					{
						Console.ForegroundColor = ConsoleColor.White;
					}
					else
					{
						Console.ForegroundColor = ConsoleColor.Gray;
					}

					Console.Write(toPrint.GetText(data));
				}
				SetCursorPosition(0, currentPosition.Top + maxRow + 1);

				Console.ForegroundColor = currentForeColor;
				Console.BackgroundColor = currentBackColor;
			}
		}

		public void RenderItems(IEnumerable<DisplayItem> items, DataRow data)
		{
			if (items.Any())
			{
				var currentPosition = Console.GetCursorPosition();
				int maxRow = 0;
				foreach (var toPrint in items)
				{
					SetCursorPosition(toPrint.DisplayFormat.Column, currentPosition.Top + toPrint.DisplayFormat.Row);
					if (toPrint.DisplayFormat.Row > maxRow) { maxRow = toPrint.DisplayFormat.Row; }

					if (toPrint.DisplayFormat.Bold)
					{
						Console.ForegroundColor = ConsoleColor.White;
					}
					else
					{
						Console.ForegroundColor = ConsoleColor.Gray;
					}

					Console.Write(toPrint.GetText(data));
				}
				SetCursorPosition(0, currentPosition.Top + maxRow + 1);
			}
		}

		private void SetCursorPosition(int x, int y)
		{
			Console.SetCursorPosition(x, Math.Min(Console.BufferHeight - 1, y));
		}
	}
}