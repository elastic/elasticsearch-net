using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace Nest.Tests.Unit.Cluster
{
	[TestFixture]
	public class PendingTasksTests : BaseJsonTests
	{
		[Test]
		public void RequestUrlTest()
		{
			var r = this._client.ClusterPendingTasks();
			var status = r.ConnectionStatus;
			var url = new Uri(status.RequestUrl);
			url.AbsolutePath.Should().StartWith("/_cluster/pending_tasks"); 
		}

		[Test]
		public void ResponseBodyJsonTest()
		{
			var json = @"{
				tasks: [
					{
						insert_order: 101,
						priority: ""URGENT"",
						source: ""create-index [foo_9], cause [api]"",
						time_in_queue_millis: 86,
						time_in_queue: ""86ms""
					},
					{
						insert_order: 46,
						priority: ""HIGH"",
						source: ""shard-started ([foo_2][1], node[tMTocMvQQgGCkj7QDHl3OA], [P], s[INITIALIZING]), reason [after recovery from gateway]"",
						time_in_queue_millis: 842,
						time_in_queue: ""842ms""
					}
				]
			}";

			var bytes = System.Text.Encoding.UTF8.GetBytes(json);
			var stream = new MemoryStream(bytes); 

			var response = _client.Serializer.Deserialize<ClusterPendingTasksResponse>(stream);
			response.Should().NotBeNull();
			response.Tasks.Count().ShouldBeEquivalentTo(2);
			
			var task1 = response.Tasks.ElementAt(0);
			task1.InsertOrder.ShouldBeEquivalentTo(101);
			task1.Priority.ShouldBeEquivalentTo("URGENT");
			task1.Source.ShouldBeEquivalentTo("create-index [foo_9], cause [api]");
			task1.TimeInQueueMilliseconds.ShouldBeEquivalentTo(86);
			task1.TimeInQueue.ShouldBeEquivalentTo("86ms");

			var task2 = response.Tasks.ElementAt(1);
			task2.InsertOrder.ShouldBeEquivalentTo(46);
			task2.Priority.ShouldBeEquivalentTo("HIGH");
			task2.Source.ShouldBeEquivalentTo("shard-started ([foo_2][1], node[tMTocMvQQgGCkj7QDHl3OA], [P], s[INITIALIZING]), reason [after recovery from gateway]");
			task2.TimeInQueueMilliseconds.ShouldBeEquivalentTo(842);
			task2.TimeInQueue.ShouldBeEquivalentTo("842ms");
		}
	}
}