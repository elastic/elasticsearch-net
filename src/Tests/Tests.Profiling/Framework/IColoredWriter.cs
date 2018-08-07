using System;

namespace Tests.Profiling.Framework
{
	public interface IColoredWriter
	{
		void Write(ConsoleColor color, string value);
		void WriteLine(ConsoleColor color, string value);
	}
}
