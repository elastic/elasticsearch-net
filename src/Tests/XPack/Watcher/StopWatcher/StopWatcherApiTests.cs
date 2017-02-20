using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.Watcher.StopWatcher
{
	public class StopWatcherApiTests : ApiIntegrationTestBase<WatcherStateCluster, IStopWatcherResponse, IStopWatcherRequest, StopWatcherDescriptor, StopWatcherRequest>
	{
		public StopWatcherApiTests(WatcherStateCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.StopWatcher(f),
			fluentAsync: (client, f) => client.StopWatcherAsync(f),
			request: (client, r) => client.StopWatcher(r),
			requestAsync: (client, r) => client.StopWatcherAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override string UrlPath => "/_xpack/watcher/_stop";

		protected override bool SupportsDeserialization => true;

		protected override object ExpectJson => null;

		protected override Func<StopWatcherDescriptor, IStopWatcherRequest> Fluent => f => f;

		protected override StopWatcherRequest Initializer => new StopWatcherRequest();

		protected override void ExpectResponse(IStopWatcherResponse response)
		{
			response.Acknowledged.Should().BeTrue();
		}
	}
}
