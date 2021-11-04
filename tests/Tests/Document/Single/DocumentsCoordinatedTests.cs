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

public class DocumentsCoordinatedTests : CoordinatedIntegrationTestBase<WritableCluster>
{
	private const string IndexDocumentStep = nameof(IndexDocumentStep);
	private const string DocumentExistsStep = nameof(DocumentExistsStep);
	private const string DeleteDocumentStep = nameof(DeleteDocumentStep);

	public DocumentsCoordinatedTests(WritableCluster cluster, EndpointUsage usage) : base(
		new CoordinatedUsageV2(cluster, usage)
		{
			{
				IndexDocumentStep, u =>
					u.Calls<IndexRequestDescriptor<Project>, IndexRequest<Project>, IndexResponse>(
						v => new IndexRequest<Project>(Project.Instance, v),
						(v, d) => d,
						(v, c, f) => c.Index(Project.Instance, "project", f => f.Id(v)), // TODO: Should be able to set ID in the Index method and should be able to infer the index name
						(v, c, f) => c.IndexAsync(Project.Instance, "project", f => f.Id(v)),
						(_, c, r) => c.Index(r),
						(_, c, r) => c.IndexAsync(r)
					)
			},
			{
				DocumentExistsStep, u =>
					u.Calls<ExistsRequestDescriptor, ExistsRequest, ExistsResponse>(
						v => new ExistsRequest(typeof(Project), v),
						(v, d) => d,
						(v, c, f) => c.Exists(Infer.Index<Project>(), v, f), 
						(v, c, f) => c.ExistsAsync(Infer.Index<Project>(), v, f),
						(_, c, r) => c.Exists(r),
						(_, c, r) => c.ExistsAsync(r)
					)
			},
			{
				DeleteDocumentStep, u =>
					u.Calls<DeleteRequestDescriptor, DeleteRequest, DeleteResponse>(
						v => new DeleteRequest(Infer.Index<Project>(), v),
						(v, d) => d,
						(v, c, f) => c.Delete(Infer.Index<Project>(), v, f),
						(v, c, f) => c.DeleteAsync(Infer.Index<Project>(), v, f),
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
		r.Index.Should().Be("project");
		r.Id.Should().Be(v);
		r.Version.Should().Be(1);
		r.Result.Should().Be(Result.Created);
		r.Shards.Successful.Should().BeGreaterOrEqualTo(1);
		r.Shards.Total.Should().BeGreaterOrEqualTo(1);
		r.Shards.Failed.Should().Be(0);
		r.SeqNo.Should().BeGreaterOrEqualTo(1);
		r.PrimaryTerm.Should().BeGreaterOrEqualTo(1);
	});

	[I]
	public async Task ExistsResponse() => await Assert<ExistsResponse>(DocumentExistsStep, (v, r) =>
	{
		r.IsValid.Should().BeTrue();
	});

	[I]
	public async Task DeleteResponse() => await Assert<DeleteResponse>(DeleteDocumentStep, (v, r) =>
	{
		r.IsValid.Should().BeTrue();
		r.Index.Should().Be("project");
		r.Id.Should().Be(v);
		r.Version.Should().BeGreaterOrEqualTo(1);
		r.Result.Should().Be(Result.Deleted);
		r.Shards.Successful.Should().BeGreaterOrEqualTo(1);
		r.Shards.Total.Should().BeGreaterOrEqualTo(1);
		r.Shards.Failed.Should().Be(0);
		r.SeqNo.Should().BeGreaterOrEqualTo(1);
		r.PrimaryTerm.Should().BeGreaterOrEqualTo(1);
	});
}
