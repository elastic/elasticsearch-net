using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Cluster.TaskManagement.TasksCancel
{
	[Collection(IntegrationContext.OwnIndex)]
	public class TasksCancelApiTests : ApiIntegrationTestBase<ITasksCancelResponse, ITasksCancelRequest, TasksCancelDescriptor, TasksCancelRequest>
	{
		public class Test
		{
			public long Id { get; set; }
			public string Flag { get; set; }
		}

		public TasksCancelApiTests(OwnIndexCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected IDictionary<string, TaskId> SetupTaskIds { get; } = new Dictionary<string, TaskId>();

		protected override void BeforeAllCalls(IElasticClient client, IDictionary<ClientMethod, string> values)
		{
			foreach (var index in values.Values)
			{
				client.IndexMany(Enumerable.Range(0, 10000).Select(i => new Test { Id = i + 1, Flag = "bar" }), index);
				client.Refresh(index);
			}
			foreach (var index in values.Values)
			{
				var reindex = client.ReindexOnServer(r => r
					.Source(s => s.Index(index))
					.Destination(s => s.Index($"{index}-clone"))
					.WaitForCompletion(false)
				);

				var taskId = reindex.Task;
				var taskInfo = client.TasksList(new TasksListRequest(taskId));
				taskInfo.IsValid.Should().BeTrue();
				this.SetupTaskIds[index] = taskId;
			}
		}
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.TasksCancel(f),
			fluentAsync: (client, f) => client.TasksCancelAsync(f),
			request: (client, r) => client.TasksCancel(r),
			requestAsync: (client, r) => client.TasksCancelAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override string UrlPath => $"/_reindex?refresh=true";

		protected override bool SupportsDeserialization => false;

		protected override Func<TasksCancelDescriptor, ITasksCancelRequest> Fluent => d => d
			.TaskId(this.SetupTaskIds[CallIsolatedValue]);

		protected override TasksCancelRequest Initializer => new TasksCancelRequest(this.SetupTaskIds[CallIsolatedValue]);

		protected override void ExpectResponse(ITasksCancelResponse response)
		{
			response.NodeFailures.Should().BeNullOrEmpty();
			response.Nodes.Should().NotBeEmpty();
			var tasks = response.Nodes.First().Value.Tasks;
			tasks.Should().NotBeEmpty().And.ContainKey(this.SetupTaskIds[CallIsolatedValue]);
		}
	}
}
