using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.Cluster.ClusterAllocationExplain
{
	public class ClusterAllocationExplainApiTests : ApiIntegrationTestBase<UnbalancedCluster, IClusterAllocationExplainResponse, IClusterAllocationExplainRequest, ClusterAllocationExplainDescriptor, ClusterAllocationExplainRequest>
	{
		public ClusterAllocationExplainApiTests(UnbalancedCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.ClusterAllocationExplain(f),
			fluentAsync: (client, f) => client.ClusterAllocationExplainAsync(f),
			request: (client, r) => client.ClusterAllocationExplain(r),
			requestAsync: (client, r) => client.ClusterAllocationExplainAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/_cluster/allocation/explain?include_yes_decisions=true";

		protected override object ExpectJson =>
			new
			{
				index = "project",
				shard = 0,
				primary = true
			};

		protected override Func<ClusterAllocationExplainDescriptor, IClusterAllocationExplainRequest> Fluent => s => s
			.Index<Project>()
			.Shard(0)
			.Primary()
			.IncludeYesDecisions();

		protected override ClusterAllocationExplainRequest Initializer =>
			new ClusterAllocationExplainRequest
			{
				Index = typeof(Project),
				Shard = 0,
				Primary = true,
				IncludeYesDecisions = true
			};

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

			foreach( var decision in response.CanRebalanceClusterDecisions)
			{
				decision.Decider.Should().NotBeNullOrEmpty();
				decision.Explanation.Should().NotBeNullOrEmpty();
			}

			response.CanRebalanceToOtherNode.Should().NotBeNull();
			response.RebalanceExplanation.Should().NotBeNullOrEmpty();
		}
	}
}
