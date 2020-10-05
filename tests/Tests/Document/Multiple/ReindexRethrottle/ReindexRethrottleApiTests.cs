// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using Elastic.Transport;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Document.Multiple.Reindex;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
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
			ExtendedValue(TaskIdKey, reindex.Task);
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
			ExtendedValue(TaskIdKey, reindex.Task);
		}
	}

	public abstract class ReindexRethrottleApiTests
		: ApiIntegrationTestBase<ReindexCluster, ReindexRethrottleResponse, IReindexRethrottleRequest, ReindexRethrottleDescriptor,
			ReindexRethrottleRequest>
	{
		protected const string TaskIdKey = "taskId";

		protected ReindexRethrottleApiTests(ReindexCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => null;
		protected override int ExpectStatusCode => 200;

		protected override Func<ReindexRethrottleDescriptor, IReindexRethrottleRequest> Fluent => d => d
			.RequestsPerSecond(-1);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override ReindexRethrottleRequest Initializer => new ReindexRethrottleRequest(TaskId)
		{
			RequestsPerSecond = -1,
		};

		protected override bool SupportsDeserialization => false;
		protected TaskId TaskId => RanIntegrationSetup ? ExtendedValue<TaskId>(TaskIdKey) : "foo:1";

		protected override string UrlPath => $"/_reindex/{TaskId.NodeId}%3A{TaskId.TaskNumber}/_rethrottle?requests_per_second=-1";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			client.IndexMany(Project.Projects);
			client.Indices.Refresh(Index<Project>());
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.ReindexRethrottle(TaskId, f),
			(client, f) => client.ReindexRethrottleAsync(TaskId, f),
			(client, r) => client.ReindexRethrottle(r),
			(client, r) => client.ReindexRethrottleAsync(r)
		);

		protected override ReindexRethrottleDescriptor NewDescriptor() => new ReindexRethrottleDescriptor(TaskId);

		protected override void ExpectResponse(ReindexRethrottleResponse response)
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
	}
}
