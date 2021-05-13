//using System;
//using Elastic.Transport;
//using FluentAssertions;
//using Nest;
//using Tests.Core.ManagedElasticsearch.Clusters;
//using Tests.Domain;
//using Tests.Framework.EndpointTests;
//using Tests.Framework.EndpointTests.TestState;

//namespace Tests.Cluster.ClusterAllocationExplain
//{
//	public class ClusterAllocationExplainApiTests
//		: ApiIntegrationTestBase<UnbalancedCluster, ClusterAllocationExplainResponse, IAllocationExplainRequest,
//			AllocationExplainDescriptor, AllocationExplainRequest>
//	{
//		public ClusterAllocationExplainApiTests(UnbalancedCluster cluster, EndpointUsage usage) : base(cluster, usage)
//		{
//		}

//		protected override bool ExpectIsValid => true;

//		protected override object ExpectJson =>
//			new {index = "project", shard = 0, primary = true};

//		protected override int ExpectStatusCode => 200;

//		//protected override Func<ClusterAllocationExplainDescriptor, IClusterAllocationExplainRequest> Fluent => s => s
//		//	.Index<Project>()
//		//	.Shard(0)
//		//	.Primary()
//		//	.IncludeYesDecisions();

//		protected override HttpMethod HttpMethod => HttpMethod.POST;

//		protected override AllocationExplainRequest Initializer =>
//			new() {Index = typeof(Project), Shard = 0, Primary = true, IncludeYesDecisions = true};

//		protected override string ExpectedUrlPathAndQuery => "/_cluster/allocation/explain?include_yes_decisions=true";

//		protected override LazyResponses ClientUsage() => Calls(
//			//(client, f) => client.Cluster.AllocationExplain(f),
//			//(client, f) => client.Cluster.AllocationExplainAsync(f),
//			(client, r) => client.Cluster.AllocationExplain(r),
//			(client, r) => client.Cluster.AllocationExplainAsync(r)
//		);

//		protected override void ExpectResponse(ClusterAllocationExplainResponse response)
//		{
//			response.Primary.Should().BeTrue();
//			response.Shard.Should().Be(0);
//			//response.Index.Should().NotBeNullOrEmpty();
//			response.CurrentState.Should().NotBeNullOrEmpty();
//			//response.CurrentNode.Should().NotBeNull();
//			response.CanRemainOnCurrentNode.Should().NotBeNull();
//			response.CanRebalanceCluster.Should().NotBeNull();
//			//response.CanRebalanceClusterDecisions.Should().NotBeNullOrEmpty();

//			//foreach (var decision in response.CanRebalanceClusterDecisions)
//			//{
//			//	decision.Decider.Should().NotBeNullOrEmpty();
//			//	decision.Explanation.Should().NotBeNullOrEmpty();
//			//}

//			response.CanRebalanceToOtherNode.Should().NotBeNull();
//			response.RebalanceExplanation.Should().NotBeNullOrEmpty();
//		}
//	}

//	public class ClusterAllocationExplainEmptyApiTests
//		: ApiIntegrationTestBase<UnbalancedCluster, ClusterAllocationExplainResponse, IAllocationExplainRequest,
//			AllocationExplainDescriptor, AllocationExplainRequest>
//	{
//		public ClusterAllocationExplainEmptyApiTests(UnbalancedCluster cluster, EndpointUsage usage) : base(cluster,
//			usage)
//		{
//		}

//		protected override bool ExpectIsValid => true;
//		protected override int ExpectStatusCode => 200;
//		protected override HttpMethod HttpMethod => HttpMethod.POST;

//		protected override Func<AllocationExplainDescriptor, IAllocationExplainRequest> Fluent => s => s;

//		protected override AllocationExplainRequest Initializer => new();

//		protected override string ExpectedUrlPathAndQuery => "/_cluster/allocation/explain";

//		protected override LazyResponses ClientUsage() => Calls(
//			//(client, f) => client.Cluster.AllocationExplain(f),
//			//(client, f) => client.Cluster.AllocationExplainAsync(f),
//			(client, r) => client.Cluster.AllocationExplain(r),
//			(client, r) => client.Cluster.AllocationExplainAsync(r)
//		);

//		protected override void ExpectResponse(ClusterAllocationExplainResponse response)
//		{
//			response.Primary.Should().BeFalse();
//			response.Shard.Should().Be(0);
//			//response.Index.Should().NotBeNullOrEmpty();
//			response.CurrentState.Should().NotBeNullOrEmpty();
//			//response.CurrentNode.Should().NotBeNull();
//			//response.CanRebalanceClusterDecisions.Should().NotBeNullOrEmpty();

//			//foreach (var decision in response.CanRebalanceClusterDecisions)
//			//{
//			//	decision.Decider.Should().NotBeNullOrEmpty();
//			//	decision.Explanation.Should().NotBeNullOrEmpty();
//			//}

//			response.CanAllocate.Should().Be(Decision.No);
//			response.AllocateExplanation.Should().NotBeNullOrEmpty();
//		}
//	}
//}
