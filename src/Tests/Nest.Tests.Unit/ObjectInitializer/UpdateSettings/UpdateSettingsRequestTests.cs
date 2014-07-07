using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Unit.ObjectInitializer.UpdateSettings
{
	[TestFixture]
	public class UpdateSettingsRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public UpdateSettingsRequestTests()
		{
			var request = new UpdateSettingsRequest
				{
					Index = "my-index",
					NumberOfReplicas = 5,
					AutoExpandReplicas = false,
					BlocksReadOnly = true,
					BlocksRead = true,
					BlocksWrite = false,
					BlocksMetadata = true,
					RefreshInterval = "30s",
					IndexConcurrency = 5,
					Codec = "default",
					CodecBloomLoad = false,
					FailOnMergeFailure = false,
					TranslogFlushTreshHoldOps = "10s",
					TranslogFlushThresholdSize = "2048",
					TranslogFlushThresholdPeriod = "5s",
					TranslogDisableFlush = true,
					CacheFilterMaxSize = "-1",
					CacheFilterExpire = "-1",
					GatewaySnapshotInterval = "5s",
					RoutingAllocationInclude = new Dictionary<string, object> { { "tag", "value1,value2" } },
					RoutingAllocationExclude = new Dictionary<string, object> { { "tag", "value3" } },
					RoutingAllocationRequire = new Dictionary<string, object> { { "group4", "aaa" } },
					RoutingAllocationEnable = RoutingAllocationEnableOption.NewPrimaries,
					RoutingAllocationDisableAllication = false,
					RoutingAllocationDisableNewAllocation = false,
					RoutingAllocationDisableReplicaAllocation = false,
					RoutingAllocationTotalShardsPerNode = 1,
					RecoveryInitialShards = "full",
					TtlDisablePurge = true,
					GcDeletes = true,
					TranslogFsType = "simple",
					CompoundFormat = true,
					CompoundOnFlush = true,
					WarmersEnabled = true
				};

			var response = this._client.UpdateSettings(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/my-index/_settings");
			this._status.RequestMethod.Should().Be("PUT");
		}

		[Test]
		public void UpdateSettingsBody()
		{
			this.JsonEquals(this._status.Request, MethodInfo.GetCurrentMethod());
		}
	}
}
