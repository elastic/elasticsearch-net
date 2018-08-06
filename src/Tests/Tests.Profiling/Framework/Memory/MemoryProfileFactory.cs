using System;
using System.Reflection;

namespace Tests.Framework.Profiling.Memory
{
	internal class MemoryProfileFactory : ProfileFactory<MemoryAttribute>
	{
		public MemoryProfileFactory(
			string sdkPath,
			string outputPath,
			ProfilingCluster cluster,
			Assembly assembly,
			IColoredWriter output) : base(sdkPath, outputPath, cluster, assembly, output)
		{
		}

		protected override IDisposable BeginProfiling(string resultsDirectory)
		{
			return new MemoryProfile(SdkPath, resultsDirectory);
		}
	}
}
