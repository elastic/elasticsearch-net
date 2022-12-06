// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Elastic.Clients.Elasticsearch.Cluster;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using Tests.Core.Extensions;
using HttpMethod = Elastic.Transport.HttpMethod;

namespace Tests.Cluster.ClusterHealth;

public class ClusterHealthShardsApiTests
	: ApiIntegrationTestBase<ReadOnlyCluster, HealthResponse, HealthRequestDescriptor, HealthRequest>
{
	public ClusterHealthShardsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override bool ExpectIsValid => true;
	protected override int ExpectStatusCode => 200;

	protected override HttpMethod ExpectHttpMethod => HttpMethod.GET;
	protected override Action<HealthRequestDescriptor> Fluent => c => c.Level(Level.Shards);
	protected override HealthRequest Initializer => new() { Level = Level.Shards };
	protected override string ExpectedUrlPathAndQuery => "/_cluster/health?level=shards";

	protected override LazyResponses ClientUsage() => Calls(
		(client, f) => client.Cluster.Health(f),
		(client, f) => client.Cluster.HealthAsync(f),
		(client, r) => client.Cluster.Health(r),
		(client, r) => client.Cluster.HealthAsync(r)
	);

	protected override void ExpectResponse(HealthResponse response)
	{
		response.ClusterName.Should().NotBeNullOrWhiteSpace();
		response.Status.Should().NotBe(HealthStatus.Red);
		response.TimedOut.Should().BeFalse();
		response.NumberOfNodes.Should().BeGreaterOrEqualTo(1);
		response.NumberOfDataNodes.Should().BeGreaterOrEqualTo(1);
		response.ActivePrimaryShards.Should().BeGreaterOrEqualTo(1);
		response.ActiveShards.Should().BeGreaterOrEqualTo(1);
		response.ActiveShardsPercentAsNumber.Should().BePositive();
		response.DelayedUnassignedShards.Should().Be(0);
		response.NumberOfInFlightFetch.Should().BeGreaterOrEqualTo(0);
		response.TaskMaxWaitingInQueueMillis.Should().BeGreaterOrEqualTo(0);

		response.Indices.Should().NotBeNull();

		var indices = response.Indices;

		indices.Should()
			.NotBeEmpty()
			.And.ContainKey(Infer.Index<Developer>());

		indices.Should()
			.NotBeEmpty()
			.And.ContainKey("devs");

		var indexHealth = indices["devs"];
		indexHealth.ActivePrimaryShards.Should().BeGreaterThan(0);
		indexHealth.ActiveShards.Should().BeGreaterThan(0);
		indexHealth.Shards["0"].Status.Should().Be(HealthStatus.Green);
	}
}
