// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Document.Multiple.DeleteByQueryRethrottle;

public class DeleteByQueryRethrottleApiTests
	: ApiIntegrationTestBase<ReindexCluster, DeleteByQueryRethrottleResponse, DeleteByQueryRethrottleRequestDescriptor,
		DeleteByQueryRethrottleRequest>
{
	protected const string TaskIdKey = "taskId";

	public DeleteByQueryRethrottleApiTests(ReindexCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override bool ExpectIsValid => true;

	protected override object ExpectJson => null;
	protected override int ExpectStatusCode => 200;

	protected override Action<DeleteByQueryRethrottleRequestDescriptor> Fluent => d => d
		.RequestsPerSecond(-1);

	protected override HttpMethod ExpectHttpMethod => HttpMethod.POST;

	protected override DeleteByQueryRethrottleRequest Initializer => new(TaskId.ToString())
	{
		RequestsPerSecond = -1,
	};

	protected override DeleteByQueryRethrottleRequestDescriptor NewDescriptor() => new(TaskId.ToString());

	protected override bool SupportsDeserialization => false;
	protected TaskId TaskId => RanIntegrationSetup ? ExtendedValue<TaskId>(TaskIdKey) : "foo:1";

	protected override string ExpectedUrlPathAndQuery => $"/_delete_by_query/{TaskId.NodeId}%3A{TaskId.TaskNumber}/_rethrottle?requests_per_second=-1";

	protected override void IntegrationSetup(ElasticsearchClient client, CallUniqueValues values)
	{
		foreach (var callUniqueValue in values)
		{
			client.IndexMany(Project.Projects, callUniqueValue.Value);
			client.Indices.Refresh(CallIsolatedValue);
		}
	}

	protected override LazyResponses ClientUsage() => Calls(
		(client, f) => client.DeleteByQueryRethrottle(TaskId.ToString(), f),
		(client, f) => client.DeleteByQueryRethrottleAsync(TaskId.ToString(), f),
		(client, r) => client.DeleteByQueryRethrottle(r),
		(client, r) => client.DeleteByQueryRethrottleAsync(r)
	);

	protected override void OnBeforeCall(ElasticsearchClient client)
	{
		client.IndexMany(Project.Projects, CallIsolatedValue);
		client.Indices.Refresh(CallIsolatedValue);

		var deleteByQuery = client.DeleteByQuery<Project>(CallIsolatedValue, u => u
			.Conflicts(Conflicts.Proceed)
			.Query(q => q.MatchAll())
			.Refresh()
			.RequestsPerSecond(1)
			.WaitForCompletion(false)
		);

		deleteByQuery.ShouldBeValid();
		ExtendedValue(TaskIdKey, deleteByQuery.Task);
	}

	protected override void ExpectResponse(DeleteByQueryRethrottleResponse response)
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

		task.StartTimeInMillis.Should().BeGreaterThan(0);
		task.RunningTimeInNanos.Should().BeGreaterThan(0);
		task.Cancellable.Should().BeTrue();
	}
}
