using System;
using System.Reflection;

namespace Tests.Profiling.Framework.Timeline
{
	internal class TimelineProfileFactory : ProfileFactory<TimelineAttribute>
	{
		public TimelineProfileFactory(string sdkPath, string outputPath, ProfilingCluster cluster, Assembly assembly, IColoredWriter output)
			: base(sdkPath, outputPath, cluster, assembly, output)
			{ }

		protected override IDisposable BeginProfiling(string resultsDirectory) => new TimelineProfile(this.SdkPath, resultsDirectory);
	}
}
