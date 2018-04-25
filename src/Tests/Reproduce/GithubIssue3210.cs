using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;
using FluentAssertions;

namespace Tests.Reproduce
{
	public class GithubIssue3210
	{
		private class Example
		{
		}

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

		[U]
		public async Task MissingNodeDecisionOptionsInResponseThrowExceptionWhenAttemptingToDeserializeResponse()
		{
			var client = TestClient.GetInMemoryClient();
			var stream = new MemoryStream();
			var writer = new StreamWriter(stream);
			writer.Write(ClusterAllocationResponse);
			writer.Flush();
			stream.Position = 0;
			var response = client.Serializer.Deserialize<ClusterAllocationExplainResponse>(stream);
			response.NodeAllocationDecisions.FirstOrDefault().NodeDecision.Should().NotBeNull().And.Be(Decision.WorseBalance);
		}
	}
}
