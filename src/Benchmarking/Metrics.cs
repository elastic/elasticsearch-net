using System.Diagnostics;

namespace Benchmarking
{
	public class Metrics
	{
		public static Metrics Create()
		{
			var process = Process.GetCurrentProcess();
			var baseThreadCount = process.Threads.Count;
			var baseMemorySize = process.VirtualMemorySize64;
			return new Metrics
			{
				ThreadCount = baseThreadCount,
				MemorySize = baseMemorySize
			};
		}

		public long MemorySize { get; set; }

		public int ThreadCount { get; set; }
	}
}