// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using Elastic.Clients.Elasticsearch;
using Tests.Domain;

namespace Tests.Document.Single;

public class IndexDocumentsCoordinatedTests : CoordinatedIntegrationTestBase<WritableCluster>
{
	private const string IndexDocumentStep = nameof(IndexDocumentStep);
	private const string DeleteDocumentStep = nameof(DeleteDocumentStep);

	public IndexDocumentsCoordinatedTests(WritableCluster cluster, EndpointUsage usage) : base(
		new CoordinatedUsageV2(cluster, usage)
		{
			{
				IndexDocumentStep, u =>
					u.Calls<IndexRequestDescriptor<Project>, IndexRequest<Project>, IndexResponse>(
						v => new IndexRequest<Project>(Project.Instance, v),
						(v, d) => d,
						(v, c, f) => c.Index(Project.Instance, "project", f => f.Id(v)), // TODO: Should be able to set ID in the Index method and should be able to infer the index
						(v, c, f) => c.IndexAsync(Project.Instance, "project", f => f.Id(v)),
						(_, c, r) => c.Index(r),
						(_, c, r) => c.IndexAsync(r)
					)
			},
			{
				DeleteDocumentStep, u =>
					u.Calls<DeleteRequestDescriptor, DeleteRequest, DeleteResponse>(
						v => new DeleteRequest("project", v), // TODO - Infer
						(v, d) => d,
						(v, c, f) => c.Delete("project", v, f), // TODO - Infer
						(v, c, f) => c.DeleteAsync("project", v, f), // TODO - Infer
						(_, c, r) => c.Delete(r),
						(_, c, r) => c.DeleteAsync(r)
					)
			},
		})
	{
	}

	[I]
	public async Task CreateResponse() => await Assert<IndexResponse>(IndexDocumentStep, (v, r) =>
	{
		r.IsValid.Should().BeTrue();
		//r.Index.Should().Be(v);
		//r.Acknowledged.Should().BeTrue();
		//r.ShardsAcknowledged.Should().BeTrue();
	});

	[I]
	public async Task DeleteResponse() => await Assert<DeleteResponse>(DeleteDocumentStep, (v, r) =>
	{
		r.IsValid.Should().BeTrue();
		//r.ForcedRefresh.Should().NotBeTrue();
		//r.Index.Should().Be(v);
		//r.Acknowledged.Should().BeTrue();
		//r.ShardsAcknowledged.Should().BeTrue();
	});
}
