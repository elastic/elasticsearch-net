using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.Watcher.RestartWatcher
{
	public class RestartWatcherApiTests : ApiIntegrationTestBase<WatcherStateCluster, IRestartWatcherResponse, IRestartWatcherRequest, RestartWatcherDescriptor, RestartWatcherRequest>
	{
		public RestartWatcherApiTests(WatcherStateCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.RestartWatcher(f),
			fluentAsync: (client, f) => client.RestartWatcherAsync(f),
			request: (client, r) => client.RestartWatcher(r),
			requestAsync: (client, r) => client.RestartWatcherAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override string UrlPath => "/_xpack/watcher/_restart";

		protected override bool SupportsDeserialization => true;

		protected override object ExpectJson => null;

		protected override Func<RestartWatcherDescriptor, IRestartWatcherRequest> Fluent => f => f;

		protected override RestartWatcherRequest Initializer => new RestartWatcherRequest();

		protected override void ExpectResponse(IRestartWatcherResponse response)
		{
			response.Acknowledged.Should().BeTrue();
		}
	}
}
