using System.Data;
using Sandbox;

namespace Reports.Renderers
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
                    Console.SetCursorPosition(toPrint.DisplayFormat.Column, currentPosition.Top + toPrint.DisplayFormat.Row);
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
                Console.SetCursorPosition(0, currentPosition.Top + maxRow + 1);

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
                    Console.SetCursorPosition(toPrint.DisplayFormat.Column, currentPosition.Top + toPrint.DisplayFormat.Row);
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
                Console.SetCursorPosition(0, currentPosition.Top + maxRow + 1);
            }
        }
    }
}
