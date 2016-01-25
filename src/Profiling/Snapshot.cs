using System;
using JetBrains.Profiler.Windows.Api;

namespace Profiling
{
	public class Snapshot : IDisposable
	{
		private Snapshot()
		{
			if (PerformanceProfiler.IsActive)
			{
				PerformanceProfiler.Begin();
			}
		}

		public static Snapshot Create()
		{
			return new Snapshot();
		}

		public void Dispose()
		{
			if (PerformanceProfiler.IsActive)
			{
				PerformanceProfiler.EndSave();
			}
		}
	}
}