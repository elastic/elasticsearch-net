// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Linq;
using Elastic.Clients.Elasticsearch.Cluster;
using FluentAssertions;

namespace Tests.Cluster.ClusterPendingTasks
{
	public class ClusterPendingTasksResponseDeserialisationTests : SerialisationTestBase<ClusterPendingTasksResponse>
	{
		protected override string ResponseJson => @"{
   ""tasks"": [
      {
         ""insert_order"": 101,
         ""priority"": ""URGENT"",
         ""source"": ""create-index [foo_9], cause [api]"",
         ""executing"" : true,
         ""time_in_queue_millis"": 86,
         ""time_in_queue"": ""86ms""
      },
      {
         ""insert_order"": 46,
         ""priority"": ""HIGH"",
         ""source"": ""shard-started ([foo_2][1], node[tMTocMvQQgGCkj7QDHl3OA], [P], s[INITIALIZING]), reason [after recovery from shard_store]"",
         ""executing"" : false,
         ""time_in_queue_millis"": 842,
         ""time_in_queue"": ""842ms""
      },
      {
         ""insert_order"": 45,
         ""priority"": ""HIGH"",
         ""source"": ""shard-started ([foo_2][0], node[tMTocMvQQgGCkj7QDHl3OA], [P], s[INITIALIZING]), reason [after recovery from shard_store]"",
         ""executing"" : false,
         ""time_in_queue_millis"": 858,
         ""time_in_queue"": ""858ms""
      }
  ]
}";

		protected override void Validate(ClusterPendingTasksResponse response)
		{
			response.Tasks.Count.Should().Be(3);

			var firstTask = response.Tasks.First();

			firstTask.InsertOrder.Should().Be(101);
			firstTask.Priority.Should().Be("URGENT");
			firstTask.Source.Should().Be("create-index [foo_9], cause [api]");
			firstTask.Executing.Should().Be(true);
			firstTask.TimeInQueueMillis.Should().Be(86);
			firstTask.TimeInQueue.Should().Be("86ms");
		}
	}
}
