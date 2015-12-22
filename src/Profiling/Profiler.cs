using System;
using JetBrains.Profiler.Windows.Api;

namespace Profiling
{
	public class Profiler : IDisposable
	{
		private Profiler()
		{
			if (PerformanceProfiler.IsActive)
			{
				PerformanceProfiler.Start();
			}
		}

		public static Profiler Start()
		{
			return new Profiler();
		}

		public void Dispose()
		{
			if (PerformanceProfiler.IsActive)
			{
				PerformanceProfiler.Stop();
			}
		}
	}
}