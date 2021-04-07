//// Licensed to Elasticsearch B.V under one or more agreements.
//// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
//// See the LICENSE file in the project root for more information

//using System.Threading.Tasks;
//using Elastic.Elasticsearch.Xunit.XunitPlumbing;
//using FluentAssertions;
//using Nest;
//using Tests.Core.ManagedElasticsearch.Clusters;
//using Tests.Framework.EndpointTests;
//using Tests.Framework.EndpointTests.TestState;

//namespace Tests.Indices.IndexManagement
//{
//	public class IndexCoordinatedTests : CoordinatedIntegrationTestBase<WritableCluster>
//	{
//		private const string CreateIndexStep = nameof(CreateIndexStep);
//		private const string DeleteIndexStep = nameof(DeleteIndexStep);

//		public IndexCoordinatedTests(WritableCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage)
//		{
//			{
//				CreateIndexStep,  u =>
//					u.Calls<CreateIndexDescriptor, CreateIndexRequest, ICreateIndexRequest, CreateIndexResponse>(
//						v => new CreateIndexRequest(v),
//						//(v, d) => d, // TODO
//						//(v, c, f) => c.Security.PutUser($"user-{v}", f),
//						//(v, c, f) => c.Security.PutUserAsync($"user-{v}", f),
//						(_, c, r) => c.Indices.CreateIndex(r),
//						(_, c, r) => c.Indices.CreateIndexAsync(r)
//					)
//			},
//			{
//				DeleteIndexStep,  u =>
//					u.Calls<DeleteIndexDescriptor, DeleteIndexRequest, IDeleteIndexRequest, DeleteIndexResponse>(
//						v => new DeleteIndexRequest(v),
//						//(v, d) => d, // TODO
//						//(v, c, f) => c.Security.PutUser($"user-{v}", f),
//						//(v, c, f) => c.Security.PutUserAsync($"user-{v}", f),
//						(_, c, r) => c.Indices.DeleteIndex(r),
//						(_, c, r) => c.Indices.DeleteIndexAsync(r)
//					)
//			}
//		})
//		{ }

//		[I] public async Task CreateIndexResponse() => await Assert<CreateIndexResponse>(CreateIndexStep, (v, r) =>
//		{
//			r.IsValid.Should().BeTrue();
//			r.Index.Should().Be(v);
//			r.Acknowledged.Should().BeTrue();
//			r.ShardsAcknowledged.Should().BeTrue();
//		});

//		[I] public async Task DeleteIndexResponse() => await Assert<DeleteIndexResponse>(DeleteIndexStep, r =>
//		{
//			r.IsValid.Should().BeTrue();
//			r.Acknowledged.Should().BeTrue();
//		});
//	}
//}
