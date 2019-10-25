using System;
using System.Threading;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cluster.TaskManagement.GetTask
{
	public class GetTaskApiTests : ApiIntegrationTestBase<WritableCluster, GetTaskResponse, IGetTaskRequest, GetTaskDescriptor, GetTaskRequest>
	{
		private static TaskId _taskId = new TaskId("fakeid:1");

		public GetTaskApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<GetTaskDescriptor, IGetTaskRequest> Fluent => s => s;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetTaskRequest Initializer => new GetTaskRequest(_taskId);
		protected override string UrlPath => $"/_tasks/fakeid%3A1";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Tasks.GetTask(_taskId, f),
			(client, f) => client.Tasks.GetTaskAsync(_taskId, f),
			(client, r) => client.Tasks.GetTask(r),
			(client, r) => client.Tasks.GetTaskAsync(r)
		);

		protected override GetTaskDescriptor NewDescriptor() => new GetTaskDescriptor(_taskId);

		protected override void ExpectResponse(GetTaskResponse response)
		{
			response.ShouldBeValid();
			response.Task.Should().NotBeNull();
			var task = response.Task;
			task.Node.Should().NotBeNullOrEmpty();
			task.Id.Should().BeGreaterThan(0);
			task.Type.Should().Be("transport");
			task.Action.Should().Be("indices:data/write/reindex");
			task.Status.Should().NotBeNull();
			task.StartTimeInMilliseconds.Should().BeGreaterThan(0);
			task.RunningTimeInNanoseconds.Should().BeGreaterThan(0);
			task.Cancellable.Should().BeTrue();
		}

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			// get a suitable load of projects in order to get a decent task status out
			var sourceIndex = "project-get-task";
			var targetIndex = "tasks-lists-get";
			var bulkResponse = client.IndexMany(Project.Generator.Generate(10000), sourceIndex);
			if (!bulkResponse.IsValid)
				throw new Exception("failure in setting up integration");

			var createIndex = client.Indices.Create(targetIndex, i => i
				.Settings(settings => settings.Analysis(DefaultSeeder.ProjectAnalysisSettings))
				.Map<Project>(DefaultSeeder.ProjectTypeMappings)
			);
			createIndex.ShouldBeValid();

			var response = client.ReindexOnServer(r => r
				.Source(s => s.Index(sourceIndex))
				.Destination(d => d
					.Index(targetIndex)
					.OpType(OpType.Create)
				)
				.Conflicts(Conflicts.Proceed)
				.WaitForCompletion(false)
				.Refresh()
			);

			_taskId = response.Task;
		}
	}

	/// <summary>
	/// Similar to GetTaskApiTests, except the test is not performed until after the task is completed,
	/// so that the response to which the task relates is available on GetTaskResponse
	/// </summary>
	public class GetTaskApiCompletedTaskTests : GetTaskApiTests
	{
		private static TaskId _taskId = new TaskId("fakeid:1");

		public GetTaskApiCompletedTaskTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<GetTaskDescriptor, IGetTaskRequest> Fluent => s => s;
		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override GetTaskRequest Initializer => new GetTaskRequest(_taskId);
		protected override string UrlPath => $"/_tasks/fakeid%3A1";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Tasks.GetTask(_taskId, f),
			(client, f) => client.Tasks.GetTaskAsync(_taskId, f),
			(client, r) => client.Tasks.GetTask(r),
			(client, r) => client.Tasks.GetTaskAsync(r)
		);

		protected override GetTaskDescriptor NewDescriptor() => new GetTaskDescriptor(_taskId);

		protected override void ExpectResponse(GetTaskResponse response)
		{
			response.ShouldBeValid();
			response.Task.Should().NotBeNull();
			response.Completed.Should().BeTrue();

			var task = response.Task;
			task.Node.Should().NotBeNullOrEmpty();
			task.Id.Should().BeGreaterThan(0);
			task.Type.Should().Be("transport");
			task.Action.Should().Be("indices:data/write/reindex");
			task.Status.Should().NotBeNull();
			task.StartTimeInMilliseconds.Should().BeGreaterThan(0);
			task.RunningTimeInNanoseconds.Should().BeGreaterThan(0);
			task.Cancellable.Should().BeTrue();

			var reindexResponse = response.GetResponse<ReindexOnServerResponse>();
			reindexResponse.Should().NotBeNull();
			reindexResponse.Took.Should().BeGreaterThan(0);
			reindexResponse.Failures.Should().BeEmpty();
		}

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			// get a suitable load of projects in order to get a decent task status out
			var sourceIndex = "project-get-completed-task";
			var targetIndex = "tasks-lists-completed-get";
			var bulkResponse = client.IndexMany(Project.Generator.Generate(500), sourceIndex);
			if (!bulkResponse.IsValid)
				throw new Exception($"failure in setting up integration for {nameof(GetTaskApiCompletedTaskTests)}. {bulkResponse.DebugInformation}");

			var createIndex = client.Indices.Create(targetIndex, i => i
				.Settings(settings => settings.Analysis(DefaultSeeder.ProjectAnalysisSettings))
				.Map<Project>(DefaultSeeder.ProjectTypeMappings)
			);
			createIndex.ShouldBeValid();

			var response = client.ReindexOnServer(r => r
				.Source(s => s.Index(sourceIndex))
				.Destination(d => d
					.Index(targetIndex)
					.OpType(OpType.Create)
				)
				.Conflicts(Conflicts.Proceed)
				.WaitForCompletion(false)
				.Refresh()
			);

			_taskId = response.Task;

			// poll until task is complete
			var getTaskResponse = client.Tasks.GetTask(_taskId);
			var completed = getTaskResponse.Completed;
			while (!completed)
			{
				Thread.Sleep(2000);
				getTaskResponse = client.Tasks.GetTask(_taskId);
				if (getTaskResponse.IsValid)
					completed = getTaskResponse.Completed;
				else
					throw new Exception($"problem setting up completed task: {getTaskResponse.DebugInformation}");
			}
		}
	}
}
