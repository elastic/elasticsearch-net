using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Cluster.TaskManagement.TasksList
{
	[Collection(IntegrationContext.ReadOnly)]
	[SkipVersion("<2.3.0", "")]
	public class TasksListApiTests : ApiIntegrationTestBase<ITasksListResponse, ITasksListRequest, TasksListDescriptor, TasksListRequest>
	{
		public TasksListApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.TasksList(f),
			fluentAsync: (client, f) => client.TasksListAsync(f),
			request: (client, r) => client.TasksList(r),
			requestAsync: (client, r) => client.TasksListAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_tasks?actions=%2Alists%2A";

		protected override Func<TasksListDescriptor, ITasksListRequest> Fluent => s => s
			.Actions("*lists*");

		protected override TasksListRequest Initializer => new TasksListRequest
		{
			Actions = new [] { "*lists*" }
		};

		protected override void ExpectResponse(ITasksListResponse response)
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
}
