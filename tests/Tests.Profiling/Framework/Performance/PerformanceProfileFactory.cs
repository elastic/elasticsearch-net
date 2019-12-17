using System;
using System.Reflection;

namespace Tests.Profiling.Framework.Performance
{
	internal class PerformanceProfileFactory : ProfileFactory<PerformanceAttribute>
	{
		public PerformanceProfileFactory(string sdkPath, string outputPath, ProfilingCluster cluster, Assembly assembly, IColoredWriter output)
			: base(sdkPath, outputPath, cluster, assembly, output)
		{
		}

		protected override IDisposable BeginProfiling(string resultsDirectory) => new PerformanceProfile(this.SdkPath, resultsDirectory);
	}
}
