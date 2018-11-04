using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Cluster.ClusterAllocationExplain
{
	public class ClusterAllocationExplainApiTests
		: ApiIntegrationTestBase<UnbalancedCluster, IClusterAllocationExplainResponse, IClusterAllocationExplainRequest,
			ClusterAllocationExplainDescriptor, ClusterAllocationExplainRequest>
	{
		public ClusterAllocationExplainApiTests(UnbalancedCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson =>
			new
			{
				index = "project",
				shard = 0,
				primary = true
			};

		protected override int ExpectStatusCode => 200;

		protected override Func<ClusterAllocationExplainDescriptor, IClusterAllocationExplainRequest> Fluent => s => s
			.Index<Project>()
			.Shard(0)
			.Primary()
			.IncludeYesDecisions();

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override ClusterAllocationExplainRequest Initializer =>
			new ClusterAllocationExplainRequest
			{
				Index = typeof(Project),
				Shard = 0,
				Primary = true,
				IncludeYesDecisions = true
			};

		protected override string UrlPath => "/_cluster/allocation/explain?include_yes_decisions=true";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.ClusterAllocationExplain(f),
			(client, f) => client.ClusterAllocationExplainAsync(f),
			(client, r) => client.ClusterAllocationExplain(r),
			(client, r) => client.ClusterAllocationExplainAsync(r)
		);

		protected override void ExpectResponse(IClusterAllocationExplainResponse response)
		{
			response.Primary.Should().BeTrue();
			response.Shard.Should().Be(0);
			response.Index.Should().NotBeNullOrEmpty();
			response.CurrentState.Should().NotBeNullOrEmpty();
			response.CurrentNode.Should().NotBeNull();
			response.CanRemainOnCurrentNode.Should().NotBeNull();
			response.CanRebalanceCluster.Should().NotBeNull();
			response.CanRebalanceClusterDecisions.Should().NotBeNullOrEmpty();

			foreach (var decision in response.CanRebalanceClusterDecisions)
			{
				decision.Decider.Should().NotBeNullOrEmpty();
				decision.Explanation.Should().NotBeNullOrEmpty();
			}

			response.CanRebalanceToOtherNode.Should().NotBeNull();
			response.RebalanceExplanation.Should().NotBeNullOrEmpty();
		}
	}
}
