using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.XPack.Watcher.StartWatcher
{
	public class StartWatcherApiTests : ApiIntegrationTestBase<WatcherStateCluster, IStartWatcherResponse, IStartWatcherRequest, StartWatcherDescriptor, StartWatcherRequest>
	{
		public StartWatcherApiTests(WatcherStateCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.StartWatcher(f),
			fluentAsync: (client, f) => client.StartWatcherAsync(f),
			request: (client, r) => client.StartWatcher(r),
			requestAsync: (client, r) => client.StartWatcherAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override string UrlPath => "/_xpack/watcher/_start";

		protected override object ExpectJson => null;

		protected override Func<StartWatcherDescriptor, IStartWatcherRequest> Fluent => f => f;

		protected override StartWatcherRequest Initializer => new StartWatcherRequest();

		protected override void ExpectResponse(IStartWatcherResponse response)
		{
			response.Acknowledged.Should().BeTrue();
		}
	}
}
