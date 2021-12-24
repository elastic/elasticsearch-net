using Elastic.Clients.Elasticsearch.Cluster;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using HttpMethod = Elastic.Transport.HttpMethod;

namespace Tests.Cluster.ClusterHealth
{
	public class ClusterHealthApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ClusterHealthResponse, ClusterHealthRequestDescriptor, ClusterHealthRequest>
	{
		public ClusterHealthApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string ExpectedUrlPathAndQuery => "/_cluster/health";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cluster.Health(),
			(client, f) => client.Cluster.HealthAsync(),
			(client, r) => client.Cluster.Health(r),
			(client, r) => client.Cluster.HealthAsync(r)
		);

		// TODO - Update these assertions once cluster is seeded
		protected override void ExpectResponse(ClusterHealthResponse response)
		{
			response.ClusterName.Value.Should().NotBeNullOrWhiteSpace();
			response.Status.Should().NotBe(HealthStatus.Red);
			response.TimedOut.Should().BeFalse();
			response.NumberOfNodes.Should().BeGreaterOrEqualTo(1);
			response.NumberOfDataNodes.Should().BeGreaterOrEqualTo(1);
			//response.ActivePrimaryShards.Should().BeGreaterOrEqualTo(1);
			//response.ActiveShards.Should().BeGreaterOrEqualTo(1);
		}
	}

	//public class ClusterHealthShardsApiTests
	//	: ApiIntegrationTestBase<ReadOnlyCluster, ClusterHealthResponse, IClusterHealthRequest, ClusterHealthDescriptor,
	//		ClusterHealthRequest>
	//{
	//	public ClusterHealthShardsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	//	protected override bool ExpectIsValid => true;
	//	protected override int ExpectStatusCode => 200;

	//	protected override HttpMethod HttpMethod => HttpMethod.GET;
	//	protected override ClusterHealthRequest Initializer => new() { Level = Level.Shards };
	//	protected override string ExpectedUrlPathAndQuery => "/_cluster/health?level=shards";

	//	protected override LazyResponses ClientUsage() => Calls(
	//		(client, r) => client.Cluster.Health(r),
	//		(client, r) => client.Cluster.HealthAsync(r)
	//	);

	//	// TODO - Update these assertions once cluster is seeded
	//	protected override void ExpectResponse(ClusterHealthResponse response)
	//	{
	//		response.ClusterName.Should().NotBeNullOrWhiteSpace();
	//		response.Status.Should().NotBe(Health.Red);
	//		response.TimedOut.Should().BeFalse();
	//		response.NumberOfNodes.Should().BeGreaterOrEqualTo(1);
	//		response.NumberOfDataNodes.Should().BeGreaterOrEqualTo(1);
	//		//response.ActivePrimaryShards.Should().BeGreaterOrEqualTo(1);
	//		//response.ActiveShards.Should().BeGreaterOrEqualTo(1);
	//		//response.ActiveShardsPercentAsNumber.Should().BePositive();
	//		response.DelayedUnassignedShards.Should().Be(0);
	//		response.NumberOfInFlightFetch.Should().BeGreaterOrEqualTo(0);
	//		//response.TaskMaxWaitTimeInQueueInMilliseconds.Should().BeGreaterOrEqualTo(0);

	//		//response.Indices.Should()
	//		//	.NotBeEmpty()
	//		//	.And.ContainKey(Index<Developer>());

	//		//var indexHealth = response.Indices[Index<Developer>()];
	//		//indexHealth.ActivePrimaryShards.Should().BeGreaterThan(0);
	//		//indexHealth.ActiveShards.Should().BeGreaterThan(0);
	//		//indexHealth.Shards["0"].Status.Should().Be(Health.Green);
	//	}
	//}
}
