// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Client;

namespace Tests.Reproduce
{
	public class GithubIssue3210
	{
		private const string ClusterAllocationResponse = @"{
  ""index"" : ""idx"",
	""shard"" : 0,
	""primary"" : true,
	""current_state"" : ""unassigned"",
	""unassigned_info"" : {
		""reason"" : ""INDEX_CREATED"",
		""at"" : ""2017-01-04T18:08:16.600Z"",
		""last_allocation_status"" : ""no""
	},
	""can_allocate"" : ""no"",
	""allocate_explanation"" : ""cannot allocate because allocation is not permitted to any of the nodes"",
	""node_allocation_decisions"" : [
		{
			""node_id"": ""_3APDoyWQUGJC2J_LbxQVw"",
			""node_name"": ""node"",
			""transport_address"": ""10.10.10.10:9300"",
			""node_attributes"": {
				""ml.max_open_jobs"": ""10"",
				""rack_id"": ""2"",
				""ml.enabled"": ""true"",
				""machinetype"": ""warm""
			},
			""node_decision"": ""worse_balance"",
			""weight_ranking"": 4
		}
	]
}";

		[U] public void MissingNodeDecisionOptionsInResponseThrowExceptionWhenAttemptingToDeserializeResponse()
		{
			var client = FixedResponseClient.Create(ClusterAllocationResponse);
			var response = client.Cluster.AllocationExplain();

			var nodeAllocationDecisions = response.NodeAllocationDecisions;
			nodeAllocationDecisions.Should().NotBeNullOrEmpty();
			nodeAllocationDecisions.First().NodeDecision.Should().NotBeNull().And.Be(Decision.WorseBalance);
		}
	}
}
