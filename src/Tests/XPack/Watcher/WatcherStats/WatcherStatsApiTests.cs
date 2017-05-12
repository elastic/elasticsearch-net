using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.Watcher.WatcherStats
{
	public class WatcherStatsApiTests : ApiIntegrationTestBase<XPackCluster, IWatcherStatsResponse, IWatcherStatsRequest, WatcherStatsDescriptor, WatcherStatsRequest>
	{
		public WatcherStatsApiTests(XPackCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				var putWatchResponse = client.PutWatch(callUniqueValue.Value, p => p
					.Active()
					.Input(i => i
						.Simple(s => s
							.Add("payload", new { send = "yes" })
						)
					)
					.Trigger(t => t
						.Schedule(s => s
							.Interval("1s")
						)
					)
					.Actions(a => a
						.Index("test_index", i => i
							.ThrottlePeriod("1s")
							.Index("test-" + CallIsolatedValue)
							.DocType("acknowledgement")
						)
					)
				);

				if (!putWatchResponse.IsValid)
					throw new Exception("Problem setting up PutWatch for integration test");
			}
		}

		protected override void OnBeforeCall(IElasticClient client)
		{
			var executeWatchResponse = client.ExecuteWatch(e => e
				.Id(CallIsolatedValue)
				.TriggerData(tr => tr
					.TriggeredTime("now")
					.ScheduledTime("now")
				)
				.ActionModes(f => f
					.Add("_all", ActionExecutionMode.Execute)
				)
				.RecordExecution()
			);

			if (!executeWatchResponse.IsValid)
				throw new Exception($"Problem with ExecuteWatch for integration test: {executeWatchResponse.DebugInformation}");
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.WatcherStats(f),
			fluentAsync: (client, f) => client.WatcherStatsAsync(f),
			request: (client, r) => client.WatcherStats(r),
			requestAsync: (client, r) => client.WatcherStatsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override string UrlPath => "/_xpack/watcher/stats/_all";

		protected override bool SupportsDeserialization => true;

		protected override object ExpectJson => null;

		protected override Func<WatcherStatsDescriptor, IWatcherStatsRequest> Fluent => f => f
			.WatcherStatsMetric(WatcherStatsMetric.All);

		protected override WatcherStatsRequest Initializer => new WatcherStatsRequest(WatcherStatsMetric.All);

		protected override void ExpectResponse(IWatcherStatsResponse response)
		{
			response.ClusterName.Should().NotBeNullOrWhiteSpace();
			response.Stats.Should().NotBeEmpty();
			var nodeStats = response.Stats.First();

			nodeStats.WatchCount.Should().BeGreaterThan(0);
			nodeStats.WatcherState.Should().Be(WatcherState.Started);

			nodeStats.ExecutionThreadPool.Should().NotBeNull();

			// TODO: Would be good if we can test these too
			nodeStats.CurrentWatches.Should().NotBeNull();
			nodeStats.QueuedWatches.Should().NotBeNull();
		}
	}
}
