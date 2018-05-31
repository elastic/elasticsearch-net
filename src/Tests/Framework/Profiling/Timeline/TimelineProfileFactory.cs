using System;
using System.Reflection;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Framework.Profiling.Timeline
{
	internal class TimelineProfileFactory : ProfileFactory<TimelineAttribute>
	{
		public TimelineProfileFactory(
			string sdkPath,
			string outputPath,
			ProfilingCluster cluster,
			Assembly assembly,
			IColoredWriter output) : base(sdkPath, outputPath, cluster, assembly, output)
		{ }

		protected override IDisposable BeginProfiling(string resultsDirectory)
		{
			return new TimelineProfile(SdkPath, resultsDirectory);
		}
	}
}
