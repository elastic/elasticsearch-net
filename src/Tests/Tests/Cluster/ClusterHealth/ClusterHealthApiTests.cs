using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using static Nest.Infer;

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
			response.Status.Should().NotBe(Health.Red);
			response.TimedOut.Should().BeFalse();
			response.NumberOfNodes.Should().BeGreaterOrEqualTo(1);
			response.NumberOfDataNodes.Should().BeGreaterOrEqualTo(1);
			response.ActivePrimaryShards.Should().BeGreaterOrEqualTo(1);
			response.ActiveShards.Should().BeGreaterOrEqualTo(1);
		}
	}

	public class ClusterHealthShardsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IClusterHealthResponse, IClusterHealthRequest, ClusterHealthDescriptor, ClusterHealthRequest>
	{
		public ClusterHealthShardsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<ClusterHealthDescriptor, IClusterHealthRequest> Fluent => c => c.Level(Level.Shards);
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override ClusterHealthRequest Initializer => new ClusterHealthRequest { Level = Level.Shards };
		protected override string UrlPath => "/_cluster/health?level=shards";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.ClusterHealth(f),
			(client, f) => client.ClusterHealthAsync(f),
			(client, r) => client.ClusterHealth(r),
			(client, r) => client.ClusterHealthAsync(r)
		);

		protected override void ExpectResponse(IClusterHealthResponse response)
		{
			response.ClusterName.Should().NotBeNullOrWhiteSpace();
			response.Status.Should().NotBe(Health.Red);
			response.TimedOut.Should().BeFalse();
			response.NumberOfNodes.Should().BeGreaterOrEqualTo(1);
			response.NumberOfDataNodes.Should().BeGreaterOrEqualTo(1);
			response.ActivePrimaryShards.Should().BeGreaterOrEqualTo(1);
			response.ActiveShards.Should().BeGreaterOrEqualTo(1);
			response.Indices.Should()
				.NotBeEmpty()
				.And.ContainKey(Index<Developer>());

			var indexHealth = response.Indices[Index<Developer>()];
			indexHealth.ActivePrimaryShards.Should().BeGreaterThan(0);
			indexHealth.ActiveShards.Should().BeGreaterThan(0);
			indexHealth.Shards["0"].Status.Should().Be(Health.Green);
		}
	}
}
