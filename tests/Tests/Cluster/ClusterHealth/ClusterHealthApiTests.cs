// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Clients.Elasticsearch.Cluster;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using HttpMethod = Elastic.Transport.HttpMethod;

namespace Tests.Cluster.ClusterHealth;

public class ClusterHealthApiTests
	: ApiIntegrationTestBase<ReadOnlyCluster, ClusterHealthResponse, ClusterHealthRequestDescriptor, ClusterHealthRequest>
{
	public ClusterHealthApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override bool ExpectIsValid => true;
	protected override int ExpectStatusCode => 200;
	protected override HttpMethod HttpMethod => HttpMethod.GET;
	protected override string ExpectedUrlPathAndQuery => "/_cluster/health";

	protected override LazyResponses ClientUsage() => Calls(
		(client, f) => client.Cluster.Health(f),
		(client, f) => client.Cluster.HealthAsync(f),
		(client, r) => client.Cluster.Health(r),
		(client, r) => client.Cluster.HealthAsync(r)
	);

	protected override void ExpectResponse(ClusterHealthResponse response)
	{
		response.ClusterName.Value.Should().NotBeNullOrWhiteSpace();
		response.Status.Should().NotBe(HealthStatus.Red);
		response.TimedOut.Should().BeFalse();
		response.NumberOfNodes.Should().BeGreaterOrEqualTo(1);
		response.NumberOfDataNodes.Should().BeGreaterOrEqualTo(1);
		response.ActivePrimaryShards.Should().BeGreaterOrEqualTo(1);
		response.ActiveShards.Should().BeGreaterOrEqualTo(1);
	}
}
