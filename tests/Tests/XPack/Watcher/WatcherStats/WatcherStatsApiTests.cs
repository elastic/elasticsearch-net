// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using Elastic.Transport;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Watcher.WatcherStats
{
	public class WatcherStatsApiTests
		: ApiIntegrationTestBase<WatcherCluster, WatcherStatsResponse, IWatcherStatsRequest, WatcherStatsDescriptor, WatcherStatsRequest>
	{
		public WatcherStatsApiTests(WatcherCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;

		protected override Func<WatcherStatsDescriptor, IWatcherStatsRequest> Fluent => f => f
			.Metric(WatcherStatsMetric.All);

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override WatcherStatsRequest Initializer => new WatcherStatsRequest(WatcherStatsMetric.All);

		protected override string UrlPath => "/_watcher/stats/_all";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				var putWatchResponse = client.Watcher.Put(callUniqueValue.Value, p => p
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
						)
					)
				);

				if (!putWatchResponse.IsValid)
					throw new Exception("Problem setting up PutWatch for integration test");
			}
		}

		protected override void OnBeforeCall(IElasticClient client)
		{
			var executeWatchResponse = client.Watcher.Execute(e => e
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
			(client, f) => client.Watcher.Stats(f),
			(client, f) => client.Watcher.StatsAsync(f),
			(client, r) => client.Watcher.Stats(r),
			(client, r) => client.Watcher.StatsAsync(r)
		);

		protected override void ExpectResponse(WatcherStatsResponse response)
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
