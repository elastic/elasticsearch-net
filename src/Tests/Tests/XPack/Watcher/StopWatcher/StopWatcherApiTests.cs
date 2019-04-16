using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.XPack.Watcher.StopWatcher
{
	public class StopWatcherApiTests
		: ApiIntegrationTestBase<WatcherStateCluster, IStopWatcherResponse, IStopWatcherRequest, StopWatcherDescriptor, StopWatcherRequest>
	{
		public StopWatcherApiTests(WatcherStateCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;

		protected override Func<StopWatcherDescriptor, IStopWatcherRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override StopWatcherRequest Initializer => new StopWatcherRequest();

		protected override string UrlPath => "/_watcher/_stop";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.StopWatcher(f),
			(client, f) => client.StopWatcherAsync(f),
			(client, r) => client.StopWatcher(r),
			(client, r) => client.StopWatcherAsync(r)
		);

		protected override void ExpectResponse(IStopWatcherResponse response) => response.Acknowledged.Should().BeTrue();
	}
}
