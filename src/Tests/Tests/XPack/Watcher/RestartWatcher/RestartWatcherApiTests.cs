using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.XPack.Watcher.RestartWatcher
{
	public class RestartWatcherApiTests
		: ApiIntegrationTestBase<WatcherStateCluster, IRestartWatcherResponse, IRestartWatcherRequest, RestartWatcherDescriptor, RestartWatcherRequest
		>
	{
		public RestartWatcherApiTests(WatcherStateCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;

		protected override Func<RestartWatcherDescriptor, IRestartWatcherRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override RestartWatcherRequest Initializer => new RestartWatcherRequest();

		protected override string UrlPath => "/_xpack/watcher/_restart";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.RestartWatcher(f),
			(client, f) => client.RestartWatcherAsync(f),
			(client, r) => client.RestartWatcher(r),
			(client, r) => client.RestartWatcherAsync(r)
		);

		protected override void ExpectResponse(IRestartWatcherResponse response) => response.Acknowledged.Should().BeTrue();
	}
}
