using System;

namespace Tests.Framework.Profiling
{
	public interface IColoredWriter
	{
		void Write(ConsoleColor color, string value);
		void WriteLine(ConsoleColor color, string value);
	}
}
