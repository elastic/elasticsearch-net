// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Document.Multiple;

public class BulkUpdateManyTests : NdJsonApiIntegrationTestBase<WritableCluster, BulkResponse, BulkRequestDescriptor, BulkRequest>
{
	private readonly List<Project> _updates = Project.Projects.Take(10).ToList();

	public BulkUpdateManyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override IReadOnlyList<object> ExpectNdjson => _updates.SelectMany(ProjectToBulkJson).ToList();

	private IEnumerable<object> ProjectToBulkJson(Project p)
	{
		yield return new Dictionary<string, object> { { "update", new { _id = p.Name, routing = p.Name } } };
		yield return new { script = new { source = "ctx._source.numberOfContributors++" } };
	}

	protected override Action<BulkRequestDescriptor> Fluent => d => d
		.Index(Infer.Index<Project>())
		.UpdateMany(_updates, (b, o) => b.Script(s => s.Source("ctx._source.numberOfContributors++")));

	protected override HttpMethod HttpMethod => HttpMethod.POST;

	protected override BulkRequest Initializer => new(Infer.Index<Project>())
	{
		Operations = _updates
			.Select(u => new BulkUpdateOperation<Project, Project>(u) { Script = new Script(new InlineScript("ctx._source.numberOfContributors++")) })
			.ToList<IBulkOperation>()
	};

	protected override bool SupportsDeserialization => false;
	protected override string ExpectedUrlPathAndQuery => $"/project/_bulk";
	protected override bool ExpectIsValid => true;
	protected override int ExpectStatusCode => 200;

	protected override LazyResponses ClientUsage() => Calls(
		(client, f) => client.Bulk(f),
		(client, f) => client.BulkAsync(f),
		(client, r) => client.Bulk(r),
		(client, r) => client.BulkAsync(r)
	);

	protected override void ExpectResponse(BulkResponse response)
	{
		response.Took.Should().BeGreaterThan(0);
		response.Errors.Should().BeFalse();
	}
}
