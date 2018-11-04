using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Cluster.TaskManagement.TasksCancel
{
	public class TasksCancelApiTests
		: ApiIntegrationTestBase<IntrusiveOperationCluster, ICancelTasksResponse, ICancelTasksRequest, CancelTasksDescriptor, CancelTasksRequest>
	{
		public TasksCancelApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;


		protected override Func<CancelTasksDescriptor, ICancelTasksRequest> Fluent => d => d
			.TaskId(TaskId);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override CancelTasksRequest Initializer => new CancelTasksRequest(TaskId);
		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_tasks/{U(TaskId.ToString())}/_cancel";
		private TaskId TaskId => RanIntegrationSetup ? ExtendedValue<TaskId>("taskId") : "foo:1";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var index in values.Values)
			{
				client.IndexMany(Enumerable.Range(0, 10000).Select(i => new Test { Id = i + 1, Flag = "bar" }), index);
				client.Refresh(index);
			}
			foreach (var view in values.Views)
			{
				values.CurrentView = view;
				var index = values.Value;

				var reindex = client.ReindexOnServer(r => r
					.Source(s => s.Index(index))
					.Destination(s => s.Index($"{index}-clone"))
					.WaitForCompletion(false)
				);

				var taskId = reindex.Task;
				var taskInfo = client.GetTask(taskId);
				taskInfo.ShouldBeValid();
				values.ExtendedValue("taskId", taskId);
			}
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.CancelTasks(f),
			(client, f) => client.CancelTasksAsync(f),
			(client, r) => client.CancelTasks(r),
			(client, r) => client.CancelTasksAsync(r)
		);

		protected override void ExpectResponse(ICancelTasksResponse response)
		{
			response.NodeFailures.Should().BeNullOrEmpty();
			response.Nodes.Should().NotBeEmpty();
			var tasks = response.Nodes.First().Value.Tasks;
			tasks.Should().NotBeEmpty().And.ContainKey(TaskId);
		}

		private class Test
		{
			public string Flag { get; set; }
			public long Id { get; set; }
		}
	}
}
