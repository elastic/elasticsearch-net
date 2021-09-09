using System.IO;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Cluster;

namespace Tests.Serialization
{
	//public class CreateIndexRequestTests
	//{
	//	[U]
	//	public async Task Serialize()
	//	{
	//		var request = new IndicesCreateRequest("index-name")
	//		{
	//			Mappings = new TypeMapping
	//			{
	//				DateDetection = false,
	//				Meta = new Metadata {{"foo", "bar"}},
	//				Properties = new Dictionary<PropertyName, PropertyBase>
	//				{
	//					{"age", new NumberProperty {Type = NumberType.Integer}},
	//					{"name", new TextProperty {IgnoreAbove = 10}},
	//					{"email", new KeywordProperty()}
	//				}
	//			}
	//		};

	//		var serializer = new DefaultHighLevelSerializer(new ElasticsearchClientSettings());

	//		var ms = new MemoryStream();

	//		await serializer.SerializeAsync(request, ms);

	//		// Only serialising the base type! Support polymorphic
	//		var jsonString = Encoding.Default.GetString(ms.ToArray());
	//	}
	//}

	public class ClusterResponseTests
	{
		[U]
		public void ClusterHealthResponse()
		{
			const string responseJson = @"{
  ""cluster_name"" : ""test-cluster"",
  ""status"" : ""green"",
  ""timed_out"" : false,
  ""number_of_nodes"" : 3,
  ""number_of_data_nodes"" : 2,
  ""active_primary_shards"" : 19,
  ""active_shards"" : 38,
  ""relocating_shards"" : 11,
  ""initializing_shards"" : 21,
  ""unassigned_shards"" : 31,
  ""delayed_unassigned_shards"" : 5,
  ""number_of_pending_tasks"" : 6,
  ""number_of_in_flight_fetch"" : 7,
  ""task_max_waiting_in_queue_millis"" : 56,
  ""active_shards_percent_as_number"" : 100.0,
  ""indices"" : {
    ""issue-test"" : {
      ""status"" : ""green"",
      ""number_of_shards"" : 10,
      ""number_of_replicas"" : 5,
      ""active_primary_shards"" : 2,
      ""active_shards"" : 2,
      ""relocating_shards"" : 0,
      ""initializing_shards"" : 0,
      ""unassigned_shards"" : 0,
      ""shards"" : {
        ""0"" : {
          ""status"" : ""green"",
          ""primary_active"" : true,
          ""active_shards"" : 2,
          ""relocating_shards"" : 0,
          ""initializing_shards"" : 0,
          ""unassigned_shards"" : 0
        }
      }
    },
    ""apm-7.12.0-span-000001"" : {
      ""status"" : ""green"",
      ""number_of_shards"" : 1,
      ""number_of_replicas"" : 1,
      ""active_primary_shards"" : 1,
      ""active_shards"" : 2,
      ""relocating_shards"" : 0,
      ""initializing_shards"" : 0,
      ""unassigned_shards"" : 0,
      ""shards"" : {
        ""0"" : {
          ""status"" : ""green"",
          ""primary_active"" : true,
          ""active_shards"" : 2,
          ""relocating_shards"" : 0,
          ""initializing_shards"" : 0,
          ""unassigned_shards"" : 0
        }
      }
    }
  }
}";

			var ms = new MemoryStream(responseJson.Utf8Bytes());

			var settings = new ElasticsearchClientSettings();
			var serializer = new DefaultHighLevelSerializer(settings);

			var response = serializer.Deserialize<HealthResponse>(ms);

			response.ClusterName.Should().Be("test-cluster");
			response.Status.Should().Be(Health.Green);
			response.NumberOfNodes.Should().Be(3);
			response.NumberOfDataNodes.Should().Be(2);
			response.ActivePrimaryShards.Should().Be(19);
			response.ActiveShards.Should().Be(38);
			response.RelocatingShards.Should().Be(11);
			response.InitializingShards.Should().Be(21);
			response.UnassignedShards.Should().Be(31);
			response.DelayedUnassignedShards.Should().Be(5);
			response.NumberOfPendingTasks.Should().Be(6);
			response.NumberOfInFlightFetch.Should().Be(7);

			response.Indices.Should().HaveCount(2);

			var issueIndex = response.Indices["issue-test"];
			issueIndex.Status.Should().Be(Health.Green);
			issueIndex.NumberOfShards.Should().Be(10);

			// TODO: The types for these properties are only stubs for now
			//response.TaskMaxWaitingInQueueMillis.Should().Be(56);
			//response.ActiveShardsPercentAsNumber.Should().Be(100.0);
		}

		[U]
		public void ClusterAllocationExplainResponse()
		{
			const string responseJson = @"{
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

			var ms = new MemoryStream(responseJson.Utf8Bytes());

			var settings = new ElasticsearchClientSettings();
			var serializer = new DefaultHighLevelSerializer(settings);

			// TODO
			//var response = serializer.Deserialize<ClusterAllocationExplainResponse>(ms);
		}
	}
}
