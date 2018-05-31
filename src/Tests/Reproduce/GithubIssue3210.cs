using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using FluentAssertions;

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
			var client = TestClient.GetFixedReturnClient(ClusterAllocationResponse);
			var response = client.ClusterAllocationExplain();

			var nodeAllocationDecisions = response.NodeAllocationDecisions;
			nodeAllocationDecisions.Should().NotBeNullOrEmpty();
			nodeAllocationDecisions.First().NodeDecision.Should().NotBeNull().And.Be(Decision.WorseBalance);
		}
	}
}
