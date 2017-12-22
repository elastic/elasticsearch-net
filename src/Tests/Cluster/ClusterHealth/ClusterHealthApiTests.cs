using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.Cluster.ClusterHealth
{
	public class ClusterHealthApiTests : ApiIntegrationTestBase<ReadOnlyCluster, IClusterHealthResponse, IClusterHealthRequest, ClusterHealthDescriptor, ClusterHealthRequest>
	{
		public ClusterHealthApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.ClusterHealth(),
			fluentAsync: (client, f) => client.ClusterHealthAsync(),
			request: (client, r) => client.ClusterHealth(r),
			requestAsync: (client, r) => client.ClusterHealthAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cluster/health";

		protected override void ExpectResponse(IClusterHealthResponse response)
		{
			response.ClusterName.Should().NotBeNullOrWhiteSpace();
			response.Status.Should().NotBe(Health.Red);
			response.TimedOut.Should().BeFalse();
			response.NumberOfNodes.Should().BeGreaterOrEqualTo(1);
			response.NumberOfDataNodes.Should().BeGreaterOrEqualTo(1);
			response.ActivePrimaryShards.Should().BeGreaterOrEqualTo(1);
			response.ActiveShards.Should().BeGreaterOrEqualTo(1);
		}
	}
	public class ClusterHealthShardsApiTests : ApiIntegrationTestBase<ReadOnlyCluster, IClusterHealthResponse, IClusterHealthRequest, ClusterHealthDescriptor, ClusterHealthRequest>
	{
		public ClusterHealthShardsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.ClusterHealth(f),
			fluentAsync: (client, f) => client.ClusterHealthAsync(f),
			request: (client, r) => client.ClusterHealth(r),
			requestAsync: (client, r) => client.ClusterHealthAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cluster/health?level=shards";

		protected override Func<ClusterHealthDescriptor, IClusterHealthRequest> Fluent => c => c.Level(Level.Shards);
		protected override ClusterHealthRequest Initializer => new ClusterHealthRequest { Level = Level.Shards };

		protected override void ExpectResponse(IClusterHealthResponse response)
		{
			response.ClusterName.Should().NotBeNullOrWhiteSpace();
			response.Status.Should().NotBe(Health.Red);
			response.TimedOut.Should().BeFalse();
			response.NumberOfNodes.Should().BeGreaterOrEqualTo(1);
			response.NumberOfDataNodes.Should().BeGreaterOrEqualTo(1);
			response.ActivePrimaryShards.Should().BeGreaterOrEqualTo(1);
			response.ActiveShards.Should().BeGreaterOrEqualTo(1);
			response.Indices.Should().NotBeEmpty()
				.And.ContainKey(Index<Developer>());

			var indexHealth = response.Indices[Index<Developer>()];
			indexHealth.ActivePrimaryShards.Should().BeGreaterThan(0);
			indexHealth.ActiveShards.Should().BeGreaterThan(0);
			indexHealth.Shards["0"].Status.Should().Be(Health.Green);
		}
	}

}
