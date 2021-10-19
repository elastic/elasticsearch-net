// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using FluentAssertions;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Cluster;

namespace Tests.Cluster.ClusterHealth
{
	public class ClusterHealthResponseDeserialisationTests : SerialisationTestBase<ClusterHealthResponse>
	{
		protected override string ResponseJson => @"{
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

		protected override void Validate(ClusterHealthResponse response)
		{
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
	}
}
