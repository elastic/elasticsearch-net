using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.XPack.Watcher.StartWatcher
{
	public class StartWatcherApiTests
		: ApiIntegrationTestBase<WatcherStateCluster, IStartWatcherResponse, IStartWatcherRequest, StartWatcherDescriptor, StartWatcherRequest>
	{
		public StartWatcherApiTests(WatcherStateCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;

		protected override Func<StartWatcherDescriptor, IStartWatcherRequest> Fluent => f => f;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override StartWatcherRequest Initializer => new StartWatcherRequest();

		protected override string UrlPath => "/_watcher/_start";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.StartWatcher(f),
			(client, f) => client.StartWatcherAsync(f),
			(client, r) => client.StartWatcher(r),
			(client, r) => client.StartWatcherAsync(r)
		);

		protected override void ExpectResponse(IStartWatcherResponse response) => response.Acknowledged.Should().BeTrue();
	}
}
