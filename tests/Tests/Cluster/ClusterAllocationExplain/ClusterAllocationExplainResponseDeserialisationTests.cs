// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using FluentAssertions;
using Elastic.Clients.Elasticsearch.Cluster;

namespace Tests.Cluster.ClusterAllocationExplain
{
	public class ClusterAllocationExplainResponseDeserialisationTests : SerialisationTestBase<ClusterAllocationExplainResponse>
	{
		protected override string ResponseJson => @"{
		  ""index"" : ""test-index"",
		  ""shard"" : 0,
		  ""primary"" : true,
		  ""current_state"" : ""started"",
		  ""current_node"" : {
		    ""id"" : ""I1WCx2PlR1OKJXNNT6SgGA"",
		    ""name"" : ""instance-0000000000"",
		    ""transport_address"" : ""10.46.232.28:19827"",
		    ""attributes"" : {
		      ""logical_availability_zone"" : ""zone-0"",
		      ""server_name"" : ""instance-0000000000.d75875ccd168497a95f1e7ce21d49822"",
		      ""availability_zone"" : ""uksouth-1"",
		      ""xpack.installed"" : ""true"",
		      ""data"" : ""hot"",
		      ""instance_configuration"" : ""azure.data.highio.l32sv2"",
		      ""transform.node"" : ""true"",
		      ""region"" : ""unknown-region""
		    },
		    ""weight_ranking"" : 1
		  },
		  ""can_remain_on_current_node"" : ""yes"",
		  ""can_rebalance_cluster"" : ""yes"",
		  ""can_rebalance_to_other_node"" : ""no"",
		  ""rebalance_explanation"" : ""cannot rebalance as no target node exists that can both allocate this shard and improve the cluster balance"",
		  ""node_allocation_decisions"" : [
		    {
		      ""node_id"" : ""EX3yzhZyQvC8GPevpb_o2g"",
		      ""node_name"" : ""instance-0000000001"",
		      ""transport_address"" : ""10.46.232.27:19936"",
		      ""node_attributes"" : {
		        ""logical_availability_zone"" : ""zone-1"",
		        ""server_name"" : ""instance-0000000001.d75875ccd168497a95f1e7ce21d49822"",
		        ""availability_zone"" : ""uksouth-3"",
		        ""xpack.installed"" : ""true"",
		        ""data"" : ""hot"",
		        ""instance_configuration"" : ""azure.data.highio.l32sv2"",
		        ""transform.node"" : ""true"",
		        ""region"" : ""unknown-region""
		      },
		      ""node_decision"" : ""no"",
		      ""weight_ranking"" : 1,
		      ""deciders"" : [
		        {
		          ""decider"" : ""same_shard"",
		          ""decision"" : ""NO"",
		          ""explanation"" : ""a copy of this shard is already allocated to this node [[test-index][0], node[EX3yzhZyQvC8GPevpb_o2g], [R], s[STARTED], a[id=CgtQVTkqTBic0VYRxatctQ]]""
		        },
		        {
		          ""decider"" : ""awareness"",
		          ""decision"" : ""NO"",
		          ""explanation"" : ""there are too many copies of the shard allocated to nodes with attribute [logical_availability_zone], there are [2] total configured shard copies for this shard id and [2] total attribute values, expected the allocated shard count per attribute [2] to be less than or equal to the upper bound of the required number of shards per attribute [1]""
		        }
		      ]
		    }
		  ]
		}";

		protected override void Validate(ClusterAllocationExplainResponse response)
		{
			// TODO - Other properties

			response.Index.ToString().Should().Be("test-index");
			response.Shard.Should().Be(0);
		}
	}
}
