using System;

namespace Profiling
{
    public class ColoredConsoleWriter
    {
        public void WriteGreen(string value)
        {
            Write(ConsoleColor.Green, value);
        }

		public void Write(string value)
        {
            Write(ConsoleColor.White, value);
        }

        public void WriteOrange(string value)
        {
            Write(ConsoleColor.DarkYellow, value);
        }

        private void Write(ConsoleColor consoleColor, string value)
        {
            lock (Console.Out)
            {
                var foregroundColor = Console.ForegroundColor;
                Console.ForegroundColor = consoleColor;
                Console.WriteLine(value);
                Console.ForegroundColor = foregroundColor;
            }
        }
    }
}