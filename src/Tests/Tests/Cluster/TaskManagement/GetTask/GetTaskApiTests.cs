using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Cluster.TaskManagement.GetTask
{
	public class GetTaskApiTests : ApiIntegrationTestBase<WritableCluster, IGetTaskResponse, IGetTaskRequest, GetTaskDescriptor, GetTaskRequest>
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
			(client, f) => client.GetTask(_taskId, f),
			(client, f) => client.GetTaskAsync(_taskId, f),
			(client, r) => client.GetTask(r),
			(client, r) => client.GetTaskAsync(r)
		);

		protected override GetTaskDescriptor NewDescriptor() => new GetTaskDescriptor(_taskId);

		protected override void ExpectResponse(IGetTaskResponse response)
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

			var createIndex = client.CreateIndex(targetIndex, i => i
				.Settings(settings => settings.Analysis(DefaultSeeder.ProjectAnalysisSettings))
				.Mappings(DefaultSeeder.ProjectMappings)
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
}
