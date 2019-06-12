using System.Collections.Generic;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Cluster.ClusterState
{
	public class ClusterStateApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ClusterStateResponse, IClusterStateRequest, ClusterStateDescriptor, ClusterStateRequest>
	{
		public ClusterStateApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cluster/state";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Cluster.State(),
			(client, f) => client.Cluster.StateAsync(),
			(client, r) => client.Cluster.State(r),
			(client, r) => client.Cluster.StateAsync(r)
		);

		protected override void ExpectResponse(ClusterStateResponse response)
		{
			response.ClusterName.Should().NotBeNullOrWhiteSpace();
			response.MasterNode.Should().NotBeNullOrWhiteSpace();
			response.StateUUID.Should().NotBeNullOrWhiteSpace();
			response.Version.Should().BeGreaterThan(0);

			var masterNode = response.State["nodes"][response.MasterNode];
			var masterNodeName = masterNode["name"] as string;
			var transportAddress = masterNode["transport_address"] as string;
			masterNodeName.Should().NotBeNullOrWhiteSpace();
			transportAddress.Should().NotBeNullOrWhiteSpace();
		}
	}
}
