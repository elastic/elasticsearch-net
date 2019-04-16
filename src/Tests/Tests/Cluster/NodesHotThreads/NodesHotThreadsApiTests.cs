using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Cluster.NodesHotThreads
{
	public class NodesHotThreadsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, INodesHotThreadsResponse, INodesHotThreadsRequest, NodesHotThreadsDescriptor, NodesHotThreadsRequest
		>
	{
		public NodesHotThreadsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_nodes/hot_threads";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.NodesHotThreads(),
			(client, f) => client.NodesHotThreadsAsync(),
			(client, r) => client.NodesHotThreads(r),
			(client, r) => client.NodesHotThreadsAsync(r)
		);

		protected override void ExpectResponse(INodesHotThreadsResponse response)
		{
			response.HotThreads.Should().NotBeEmpty();
			var t = response.HotThreads.First();
			t.NodeId.Should().NotBeNullOrWhiteSpace();
			t.NodeName.Should().NotBeNullOrWhiteSpace();
			t.Hosts.Should().NotBeEmpty();
		}
	}
}
