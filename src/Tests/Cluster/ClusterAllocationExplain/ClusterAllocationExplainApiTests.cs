using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Cluster.ClusterAllocationExplain
{
	[Collection(TypeOfCluster.ReadOnly)]
	public class ClusterAllocationExplainApiTests : ApiIntegrationTestBase<IClusterAllocationExplainResponse, IClusterAllocationExplainRequest, ClusterAllocationExplainDescriptor, ClusterAllocationExplainRequest>
	{
		public ClusterAllocationExplainApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
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
			response.Shard.Primary.Should().BeTrue();
			response.Shard.Id.Should().Be(0);
			response.Shard.IndexUniqueId.Should().NotBeNullOrEmpty();
			response.Assigned.Should().BeTrue();

			foreach (var node in response.Nodes)
			{
				var explanation = node.Value;

				explanation.NodeName.Should().NotBeNullOrEmpty();
				explanation.Weight.Should().BeGreaterOrEqualTo(0);
				explanation.NodeAttributes.Should().NotBeEmpty();
				explanation.FinalDecision.Should().NotBeNullOrEmpty();
				explanation.Decisions.Should().NotBeNullOrEmpty();
			}
		}
	}
}
