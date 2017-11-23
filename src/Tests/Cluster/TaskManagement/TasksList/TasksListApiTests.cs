using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.ManagedElasticsearch.NodeSeeders;
using Tests.Framework.MockData;
using Xunit;


namespace Tests.Cluster.TaskManagement.TasksList
{
	public class TasksListApiTests : ApiIntegrationTestBase<ReadOnlyCluster, IListTasksResponse, IListTasksRequest, ListTasksDescriptor, ListTasksRequest>
	{
		public TasksListApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.ListTasks(f),
			fluentAsync: (client, f) => client.ListTasksAsync(f),
			request: (client, r) => client.ListTasks(r),
			requestAsync: (client, r) => client.ListTasksAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_tasks?actions=%2Alists%2A";

		protected override Func<ListTasksDescriptor, IListTasksRequest> Fluent => s => s
			.Actions("*lists*");

		protected override ListTasksRequest Initializer => new ListTasksRequest
		{
			Actions = new[] { "*lists*" }
		};

		protected override void ExpectResponse(IListTasksResponse response)
		{
			response.Nodes.Should().NotBeEmpty();
			var taskExecutingNode = response.Nodes.First().Value;
			taskExecutingNode.Host.Should().NotBeNullOrWhiteSpace();
			taskExecutingNode.Ip.Should().NotBeNullOrWhiteSpace();
			taskExecutingNode.Name.Should().NotBeNullOrWhiteSpace();
			taskExecutingNode.TransportAddress.Should().NotBeNullOrWhiteSpace();
			taskExecutingNode.Tasks.Should().NotBeEmpty();
			taskExecutingNode.Tasks.Count().Should().BeGreaterOrEqualTo(2);

			var task = taskExecutingNode.Tasks.Values.First(p => p.ParentTaskId != null);
			task.Action.Should().NotBeNullOrWhiteSpace();
			task.Type.Should().NotBeNullOrWhiteSpace();
			task.Id.Should().BePositive();
			task.Node.Should().NotBeNullOrWhiteSpace();
			task.RunningTimeInNanoSeconds.Should().BeGreaterThan(0);
			task.StartTimeInMilliseconds.Should().BeGreaterThan(0);
			task.ParentTaskId.Should().NotBeNull();

			var parentTask = taskExecutingNode.Tasks[task.ParentTaskId];
			parentTask.Should().NotBeNull();
			parentTask.ParentTaskId.Should().BeNull();
		}
	}

	[SkipVersion("<2.3.0", "")]
	public class TasksListDetailedApiTests : ApiIntegrationTestBase<IntrusiveOperationCluster, IListTasksResponse, IListTasksRequest, ListTasksDescriptor, ListTasksRequest>
	{
		private static TaskId _taskId = new TaskId("fakeid:1");

		public TasksListDetailedApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.ListTasks(f),
			fluentAsync: (client, f) => client.ListTasksAsync(f),
			request: (client, r) => client.ListTasks(r),
			requestAsync: (client, r) => client.ListTasksAsync(r)
		);

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			var seeder = new DefaultSeeder(this.Cluster.Node);
			seeder.SeedNode();

			// get a suitable load of projects in order to get a decent task status out
			var bulkResponse = client.IndexMany(Project.Generator.Generate(20000));
			if (!bulkResponse.IsValid)
				throw new Exception("failure in setting up integration");

			var response = client.ReindexOnServer(r => r
				.Source(s => s
					.Index(Infer.Index<Project>())
					.Type(typeof(Project))
				)
				.Destination(d => d
					.Index("tasks-list-projects")
					.OpType(OpType.Create)
				)
				.Conflicts(Conflicts.Proceed)
				.WaitForCompletion(false)
				.Refresh()
			);

			_taskId = response.Task;
		}

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/_tasks?detailed=true";

		protected override Func<ListTasksDescriptor, IListTasksRequest> Fluent => s => s
			.Detailed();

		protected override ListTasksRequest Initializer => new ListTasksRequest()
		{
			Detailed = true
		};

		protected override void ExpectResponse(IListTasksResponse response)
		{
			response.Nodes.Should().NotBeEmpty();
			var taskExecutingNode = response.Nodes.First().Value;
			taskExecutingNode.Host.Should().NotBeNullOrWhiteSpace();
			taskExecutingNode.Ip.Should().NotBeNullOrWhiteSpace();
			taskExecutingNode.Name.Should().NotBeNullOrWhiteSpace();
			taskExecutingNode.TransportAddress.Should().NotBeNullOrWhiteSpace();
			taskExecutingNode.Tasks.Should().NotBeEmpty();
			taskExecutingNode.Tasks.Count().Should().BeGreaterOrEqualTo(1);

			var task = taskExecutingNode.Tasks[_taskId];
			task.Action.Should().NotBeNullOrWhiteSpace();
			task.Type.Should().NotBeNullOrWhiteSpace();
			task.Id.Should().BePositive();
			task.Node.Should().NotBeNullOrWhiteSpace();
			task.RunningTimeInNanoSeconds.Should().BeGreaterThan(0);
			task.StartTimeInMilliseconds.Should().BeGreaterThan(0);

			var status = task.Status;
			status.Should().NotBeNull();
			status.Total.Should().BeGreaterOrEqualTo(0);
			status.Batches.Should().BeGreaterOrEqualTo(0);
		}
	}
}
