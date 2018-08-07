using System;

namespace Tests.Profiling.Framework
{
	public class ColoredConsoleWriter  : IColoredWriter
	{
		public void Write(ConsoleColor color, string value)
		{
			lock (Console.Out)
			{
				var foregroundColor = Console.ForegroundColor;
				Console.ForegroundColor = color;
				Console.Write(value);
				Console.ForegroundColor = foregroundColor;
			}
		}

		public void WriteLine(ConsoleColor color, string value)
		{
			lock (Console.Out)
			{
				var foregroundColor = Console.ForegroundColor;
				Console.ForegroundColor = color;
				Console.WriteLine(value);
				Console.ForegroundColor = foregroundColor;
			}
		}
	}
}
