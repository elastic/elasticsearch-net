// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Document.Multiple;

public class BulkInvalidApiTests : ApiIntegrationTestBase<WritableCluster, BulkResponse, BulkRequestDescriptor, BulkRequest>
{
	public BulkInvalidApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override bool ExpectIsValid => false;

	protected override IReadOnlyList<object> ExpectNdjson => new object[]
	{
			new Dictionary<string, object> { { "update", new { _id = Project.Instance.Name } } },
			new { doc = new { leadDeveloper = new { firstName = "martijn" } } },
			new Dictionary<string, object> { { "delete", new { _id = Project.Instance.Name + "1" } } },
	};

	protected override int ExpectStatusCode => 200;

	protected override Action<BulkRequestDescriptor> Fluent => d => d
		.Index(CallIsolatedValue)
		.Update<Project, object>(b => b.Doc(new { leadDeveloper = new { firstName = "martijn" } }).Id(Project.Instance.Name))
		.Delete<Project>(b => b.Id(Project.Instance.Name + "1"));

	protected override HttpMethod HttpMethod => HttpMethod.POST;

	protected override BulkRequest Initializer => new(CallIsolatedValue)
	{
		Operations = new List<IBulkOperation>
		{
			new BulkUpdateOperation<Project, object>(Project.Instance.Name)
			{
				Doc = new { leadDeveloper = new { firstName = "martijn" } }
			},
			new BulkDeleteOperation(Project.Instance.Name + "1"),
		}
	};

	protected override bool SupportsDeserialization => false;

	protected override string ExpectedUrlPathAndQuery => $"/{CallIsolatedValue}/_bulk";

	protected override LazyResponses ClientUsage() => Calls(
		(client, f) => client.Bulk(f),
		(client, f) => client.BulkAsync(f),
		(client, r) => client.Bulk(r),
		(client, r) => client.BulkAsync(r)
	);

	protected override void ExpectResponse(BulkResponse response)
	{
		response.Took.Should().BeGreaterThan(0);
		response.Errors.Should().BeTrue();

		// A delete not found is not an error (also in Elasticsearch).
		// If you do a single bulk delete on an unknown id. Errors will be false.
		response.ItemsWithErrors.Should().NotBeNull().And.HaveCount(1);
		response.Items.Should().NotBeEmpty();

		var failedUpdate = response.Items[0] as BulkUpdateResponseItem;
		failedUpdate.Should().NotBeNull();
		failedUpdate.Index.Should().Be(CallIsolatedValue);
		failedUpdate.Status.Should().Be(404);
		failedUpdate.Error.Should().NotBeNull();
		failedUpdate.Error.Type.Should().Be("document_missing_exception");
		failedUpdate.IsValid.Should().BeFalse();

		var failedDelete = response.Items[response.Items.Count - 1] as BulkDeleteResponseItem;
		failedDelete.IsValid.Should().BeTrue();
	}
}
