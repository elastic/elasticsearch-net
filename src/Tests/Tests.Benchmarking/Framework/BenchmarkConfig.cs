using System;

namespace Tests.Benchmarking.Framework
{
	public class BenchmarkConfigAttribute : Attribute
	{
		public int RunCount { get; }

		public BenchmarkConfigAttribute(int runCount = 1) => RunCount = runCount;
	}
}
