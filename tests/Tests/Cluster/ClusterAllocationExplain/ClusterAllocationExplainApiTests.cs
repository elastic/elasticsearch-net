// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using FluentAssertions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using Elastic.Clients.Elasticsearch.Cluster;
using Elastic.Clients.Elasticsearch.Cluster.AllocationExplain;

namespace Tests.Cluster.Cluster.ClusterAllocationExplain;

public class ClusterAllocationExplainApiTests
	: ApiIntegrationTestBase<UnbalancedCluster, ClusterAllocationExplainResponse, IClusterAllocationExplainRequest,
		ClusterAllocationExplainRequestDescriptor, ClusterAllocationExplainRequest>
{
	public ClusterAllocationExplainApiTests(UnbalancedCluster cluster, EndpointUsage usage) : base(cluster, usage)
	{
	}

	protected override bool ExpectIsValid => true;

	protected override object ExpectJson =>
		new { index = "project", shard = 0, primary = true };

	protected override int ExpectStatusCode => 200;

	// TODO: Generate extra descriptor methods!
	protected override Func<ClusterAllocationExplainRequestDescriptor, IClusterAllocationExplainRequest> Fluent => s => s
		//.Index<Project>()
		.Index("project")
		.Shard(0)
		.Primary(true)
		//.Primary()
		.IncludeYesDecisions(true);

	protected override HttpMethod HttpMethod => HttpMethod.POST;

	protected override ClusterAllocationExplainRequest Initializer =>
		new() { Index = typeof(Project), Shard = 0, Primary = true, IncludeYesDecisions = true };

	protected override string ExpectedUrlPathAndQuery => "/_cluster/allocation/explain?include_yes_decisions=true";

	protected override LazyResponses ClientUsage() => Calls(
		(client, f) => client.Cluster.AllocationExplain(f),
		(client, f) => client.Cluster.AllocationExplainAsync(f),
		(client, r) => client.Cluster.AllocationExplain(r),
		(client, r) => client.Cluster.AllocationExplainAsync(r)
	);

	protected override void ExpectResponse(ClusterAllocationExplainResponse response)
	{
		response.Primary.Should().BeTrue();
		response.Shard.Should().Be(0);
		response.Index.ToString().Should().NotBeNullOrEmpty();
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

public class ClusterAllocationExplainEmptyApiTests
	: ApiIntegrationTestBase<UnbalancedCluster, ClusterAllocationExplainResponse, IClusterAllocationExplainRequest,
		ClusterAllocationExplainRequestDescriptor, ClusterAllocationExplainRequest>
{
	public ClusterAllocationExplainEmptyApiTests(UnbalancedCluster cluster, EndpointUsage usage) : base(cluster,
		usage)
	{
	}

	protected override bool ExpectIsValid => true;
	protected override int ExpectStatusCode => 200;
	protected override HttpMethod HttpMethod => HttpMethod.POST;

	protected override Func<ClusterAllocationExplainRequestDescriptor, IClusterAllocationExplainRequest> Fluent => s => s;

	protected override ClusterAllocationExplainRequest Initializer => new();

	protected override string ExpectedUrlPathAndQuery => "/_cluster/allocation/explain";

	protected override LazyResponses ClientUsage() => Calls(
		(client, f) => client.Cluster.AllocationExplain(f),
		(client, f) => client.Cluster.AllocationExplainAsync(f),
		(client, r) => client.Cluster.AllocationExplain(r),
		(client, r) => client.Cluster.AllocationExplainAsync(r)
	);

	protected override void ExpectResponse(ClusterAllocationExplainResponse response)
	{
		// TODO: Add more assertions based on JSON (once seeding is completed)
		//{
		//	"index" : "project",
		//	"shard" : 0,
		//	"primary" : false,
		//	"current_state" : "unassigned",
		//	"unassigned_info" : {
		//		"reason" : "INDEX_CREATED",
		//		"at" : "2021-10-18T15:30:12.669Z",
		//		"last_allocation_status" : "no_attempt"
		//	},
		//	"can_allocate" : "no",
		//	"allocate_explanation" : "cannot allocate because allocation is not permitted to any of the nodes",
		//	"node_allocation_decisions" : [
		//	{
		//		"node_id" : "YIraJSUvSmKmWH9F9biOjg",
		//		"node_name" : "unbalanced-node-7cc24e9200",
		//		"transport_address" : "127.0.0.1:9300",
		//		"node_attributes" : {
		//					"testingcluster" : "true",
		//		"ml.machine_memory" : "17115570176",
		//		"xpack.installed" : "true",
		//		"transform.node" : "true",
		//		"ml.max_open_jobs" : "512",
		//		"ml.max_jvm_size" : "1073741824",
		//		"gateway" : "true"
		//		},
		//		"node_decision" : "no",
		//		"weight_ranking" : 1,
		//		"deciders" : [
		//		{
		//			"decider" : "same_shard",
		//			"decision" : "NO",
		//			"explanation" : "a copy of this shard is already allocated to this node [[project][0], node[YIraJSUvSmKmWH9F9biOjg], [P], s[STARTED], a[id=ldR5C8z0Ra25DYYRxdCqHg]]"
		//		}
		//		]
		//	}
		//	]
		//}

		response.Primary.Should().BeFalse();
		response.Shard.Should().Be(0);
		response.CurrentState.Should().NotBeNullOrEmpty();
		response.CanAllocate.Should().Be(Decision.No);
		response.AllocateExplanation.Should().NotBeNullOrEmpty();
	}
}
