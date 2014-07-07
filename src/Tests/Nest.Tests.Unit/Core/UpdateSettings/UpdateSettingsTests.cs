using System.Reflection;
using NUnit.Framework;

namespace Nest.Tests.Unit.Core.UpdateSettings
{
	[TestFixture]
	public class UpdateSettingsTests : BaseJsonTests
	{

		[Test]
		public void AllUpdateSettings()
		{
			var s = new UpdateSettingsDescriptor()
				.Index("myindex")
				.AutoExpandReplicas()
				.BlockReadonly()
				.BlocksMetadata()
				.BlocksRead()
				.BlocksWrite()
				.CacheFilterExpire("1m")
				.CacheFilterMaxSize("1Gb")
				.CodeBloomLoad()
				.Codec("default")
				.CompoundFormat()
				.CompoundOnFlush()
				.FailOnMergeFailure()
				.GatewaySnapshotInterval("1h")
				.GcDeletes()
				.IndexConcurrency(8)
				.NumberOfReplicas(1)
				.RecoveryInitialShards("quorum")
				.RefreshInterval("1s")
				.RoutingAllocationDisableNewAllocation()
				.RoutingAllocationDisableAllocation()
				.RoutingAllocationDisableReplicateAllocation()
				.RoutingAllocationExclude(d => d.Add("_ip", "10.0.0.1"))
				.RoutingAllocationInclude(d => d.Add("_ip", "10.0.0.1"))
				.RoutingAllocationRequire(d => d.Add("_ip", "10.0.0.1"))
				.RoutingAllocationTotalShardsPerNode(20)
				.TranslogDisableFlush()
				.TranslogFlushThresholdOps("5m")
				.TranslogFlushThresholdPeriod("5m")
				.TranslogFlushThresholdSize("1mb")
				.TranslogFsType("simple")
				.TtlDisablePurge()
				.WarmersEnabled();
			
			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}

		[Test]
		public void CanSpecifyNewAnalysisSettings()
		{
			// do note that you need to close and reopen 
			// an index before you can update analysis settings

			var s = new UpdateSettingsDescriptor()
				.NumberOfReplicas(5)
				.Analysis(a => a
					.Analyzers(an => an
						.Add("content", new CustomAnalyzer()
						{
							Tokenizer = "whitespace"
						})
					)
				);
			
			this.JsonEquals(s, MethodInfo.GetCurrentMethod());
		}
	}
}
