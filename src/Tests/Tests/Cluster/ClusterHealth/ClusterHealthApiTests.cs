using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Cluster.ClusterHealth
{
	public class ClusterHealthApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IClusterHealthResponse, IClusterHealthRequest, ClusterHealthDescriptor, ClusterHealthRequest>
	{
		public ClusterHealthApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cluster/health";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.ClusterHealth(),
			(client, f) => client.ClusterHealthAsync(),
			(client, r) => client.ClusterHealth(r),
			(client, r) => client.ClusterHealthAsync(r)
		);

		protected override void ExpectResponse(IClusterHealthResponse response)
		{
			response.ClusterName.Should().NotBeNullOrWhiteSpace();
			response.Status.Should().NotBeNullOrWhiteSpace();
			response.TimedOut.Should().BeFalse();
			response.NumberOfNodes.Should().BeGreaterOrEqualTo(1);
			response.NumberOfDataNodes.Should().BeGreaterOrEqualTo(1);
			response.ActivePrimaryShards.Should().BeGreaterOrEqualTo(1);
			response.ActiveShards.Should().BeGreaterOrEqualTo(1);
		}
	}
}
