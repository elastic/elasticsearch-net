using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Cluster.TaskManagement.TasksCancel
{
	public class TasksCancelApiTests : ApiIntegrationTestBase<IntrusiveOperationCluster, ICancelTasksResponse, ICancelTasksRequest, CancelTasksDescriptor, CancelTasksRequest>
	{
		private TaskId TaskId => this.RanIntegrationSetup ? this.ExtendedValue<TaskId>("taskId") : "foo:1";

		private class Test
		{
			public long Id { get; set; }
			public string Flag { get; set; }
		}

		public TasksCancelApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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
			fluent: (client, f) => client.CancelTasks(f),
			fluentAsync: (client, f) => client.CancelTasksAsync(f),
			request: (client, r) => client.CancelTasks(r),
			requestAsync: (client, r) => client.CancelTasksAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/_tasks/{U(this.TaskId.ToString())}/_cancel";
		protected override bool SupportsDeserialization => false;


		protected override Func<CancelTasksDescriptor, ICancelTasksRequest> Fluent => d => d
			.TaskId(this.TaskId);

		protected override CancelTasksRequest Initializer => new CancelTasksRequest(this.TaskId);

		protected override void ExpectResponse(ICancelTasksResponse response)
		{
			response.NodeFailures.Should().BeNullOrEmpty();
			response.Nodes.Should().NotBeEmpty();
			var tasks = response.Nodes.First().Value.Tasks;
			tasks.Should().NotBeEmpty().And.ContainKey(this.TaskId);
		}
	}
}
