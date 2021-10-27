// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using Elastic.Clients.Elasticsearch.IndexManagement;

namespace Tests.IndexManagement;

public class IndexCoordinatedTests : CoordinatedIntegrationTestBase<WritableCluster>
{
	private const string CreateIndexStep = nameof(CreateIndexStep);
	private const string DeleteIndexStep = nameof(DeleteIndexStep);

	public IndexCoordinatedTests(WritableCluster cluster, EndpointUsage usage) : base(
		new CoordinatedUsage(cluster, usage)
		{
				{
					CreateIndexStep, u =>
						u.Calls<CreateIndexRequestDescriptor, CreateIndexRequest, ICreateIndexRequest, CreateIndexResponse>(
							v => new CreateIndexRequest(v),
							(v, d) => d,
							(v, c, f) => c.IndexManagement.CreateIndex(v, f),
							(v, c, f) => c.IndexManagement.CreateIndexAsync(v, f),
							(_, c, r) => c.IndexManagement.CreateIndex(r),
							(_, c, r) => c.IndexManagement.CreateIndexAsync(r)
						)
				},
				{
					DeleteIndexStep, u =>
						u.Calls<DeleteIndexRequestDescriptor, DeleteIndexRequest, IDeleteIndexRequest, DeleteIndexResponse>(
							v => new DeleteIndexRequest(v),
							(v, d) => d,
							(v, c, f) => c.IndexManagement.DeleteIndex(v, f),
							(v, c, f) => c.IndexManagement.DeleteIndexAsync(v, f),
							(_, c, r) => c.IndexManagement.DeleteIndex(r),
							(_, c, r) => c.IndexManagement.DeleteIndexAsync(r)
						)
				}
		})
	{
	}

	[I]
	public async Task CreateIndexResponse() => await Assert<CreateIndexResponse>(CreateIndexStep, (v, r) =>
	{
		r.IsValid.Should().BeTrue();
		r.Index.Should().Be(v);
		r.Acknowledged.Should().BeTrue();
		r.ShardsAcknowledged.Should().BeTrue();
	});

	[I]
	public async Task DeleteIndexResponse() => await Assert<DeleteIndexResponse>(DeleteIndexStep, r =>
	{
		r.IsValid.Should().BeTrue();
		r.Acknowledged.Should().BeTrue();
	});
}


