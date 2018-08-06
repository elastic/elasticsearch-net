using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Document.Multiple.Reindex;
using Tests.Document.Multiple.ReindexOnServer;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.Document.Multiple.ReindexRethrottle
{
	public class ReindexRethrottleReindexApiTests : ReindexRethrottleApiTests
	{
		public ReindexRethrottleReindexApiTests(ReindexCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void OnBeforeCall(IElasticClient client)
		{
			var reindex = client.ReindexOnServer(r => r
				.Source(s => s
					.Index(Index<Project>())
				)
				.Destination(s => s
					.Index(CallIsolatedValue)
					.OpType(OpType.Create)
				)
				.Refresh()
				.RequestsPerSecond(1)
				.WaitForCompletion(false)
			);

			reindex.ShouldBeValid();
			this.ExtendedValue(TaskIdKey, reindex.Task);
		}
	}

	public class ReindexRethrottleUpdateByQueryTests : ReindexRethrottleApiTests
	{
		public ReindexRethrottleUpdateByQueryTests(ReindexCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void OnBeforeCall(IElasticClient client)
		{
			var reindex = client.UpdateByQuery<Project>(u => u
				.Conflicts(Conflicts.Proceed)
				.Query(q => q.MatchAll())
				.Script(s => s.Source("ctx._source.numberOfCommits+10"))
				.Refresh()
				.RequestsPerSecond(1)
				.WaitForCompletion(false)
			);

			reindex.ShouldBeValid();
			this.ExtendedValue(TaskIdKey, reindex.Task);
		}
	}

	public abstract class ReindexRethrottleApiTests
		: ApiIntegrationTestBase<ReindexCluster, IReindexRethrottleResponse, IReindexRethrottleRequest, ReindexRethrottleDescriptor, ReindexRethrottleRequest>
	{
		protected TaskId TaskId => this.RanIntegrationSetup ? this.ExtendedValue<TaskId>(TaskIdKey) : "foo:1";

		protected const string TaskIdKey = "taskId";

		protected ReindexRethrottleApiTests(ReindexCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			client.IndexMany(Project.Projects);
			client.Refresh(Index<Project>());
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Rethrottle(TaskId, f),
			fluentAsync: (client, f) => client.RethrottleAsync(TaskId, f),
			request: (client, r) => client.Rethrottle(r),
			requestAsync: (client, r) => client.RethrottleAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override string UrlPath => $"/_reindex/{TaskId.NodeId}%3A{TaskId.TaskNumber}/_rethrottle?requests_per_second=-1";

		protected override bool SupportsDeserialization => false;

		protected override ReindexRethrottleDescriptor NewDescriptor() => new ReindexRethrottleDescriptor(TaskId);

		protected override Func<ReindexRethrottleDescriptor, IReindexRethrottleRequest> Fluent => d => d
			.RequestsPerSecond(-1);

		protected override ReindexRethrottleRequest Initializer => new ReindexRethrottleRequest(TaskId)
		{
			RequestsPerSecond = -1,
		};

		protected override void ExpectResponse(IReindexRethrottleResponse response)
		{
			response.ShouldBeValid();

			response.Nodes.Should().NotBeEmpty().And.HaveCount(1);
			var node = response.Nodes.First().Value;

			node.Name.Should().NotBeNullOrEmpty();
			node.TransportAddress.Should().NotBeNullOrEmpty();
			node.Host.Should().NotBeNullOrEmpty();
			node.Ip.Should().NotBeNullOrEmpty();
			node.Roles.Should().NotBeEmpty();
			node.Attributes.Should().NotBeEmpty();

			node.Tasks.Should().NotBeEmpty().And.HaveCount(1);

			node.Tasks.First().Key.Should().Be(TaskId);

			var task = node.Tasks.First().Value;

			task.Node.Should().NotBeNullOrEmpty().And.Be(TaskId.NodeId);
			task.Id.Should().Be(TaskId.TaskNumber);
			task.Type.Should().NotBeNullOrEmpty();
			task.Action.Should().NotBeNullOrEmpty();

			task.Status.RequestsPerSecond.Should().Be(-1);

			task.StartTimeInMilliseconds.Should().BeGreaterThan(0);
			task.RunningTimeInNanoseconds.Should().BeGreaterThan(0);
			task.Cancellable.Should().BeTrue();
		}

		protected override object ExpectJson => null;
	}
}
