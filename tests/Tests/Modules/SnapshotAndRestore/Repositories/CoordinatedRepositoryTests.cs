// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Modules.SnapshotAndRestore.Repositories
{
	public class CoordinatedRepositoryTests : CoordinatedIntegrationTestBase<IntrusiveOperationCluster>
	{
		private const string AnalyzeRepositoryStep = nameof(AnalyzeRepositoryStep);
		private const string CreateRepositoryStep = nameof(CreateRepositoryStep);
		private const string DeleteRepositoryStep = nameof(DeleteRepositoryStep);

		public CoordinatedRepositoryTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage)
		{
			{
				CreateRepositoryStep, u => u
					.Calls<CreateRepositoryDescriptor, CreateRepositoryRequest, ICreateRepositoryRequest, CreateRepositoryResponse>(
						v => new CreateRepositoryRequest(v)
						{
							Repository = new FileSystemRepository(
								new FileSystemRepositorySettings(GetRepositoryPath(cluster.FileSystem.RepositoryPath, v))
								{
									ChunkSize = "64mb", Compress = true
								}
							)
						},
						(v, d) => d.FileSystem(fs => fs
							.Settings(GetRepositoryPath(cluster.FileSystem.RepositoryPath, v), s => s
								.ChunkSize("64mb")
								.Compress()
							)
						),
						(v, c, f) => c.Snapshot.CreateRepository(v, f),
						(v, c, f) => c.Snapshot.CreateRepositoryAsync(v, f),
						(_, c, r) => c.Snapshot.CreateRepository(r),
						(_, c, r) => c.Snapshot.CreateRepositoryAsync(r))
			},
			{
				DeleteRepositoryStep, u => u
					.Calls<DeleteRepositoryDescriptor, DeleteRepositoryRequest, IDeleteRepositoryRequest, DeleteRepositoryResponse>(
						v => new DeleteRepositoryRequest(v),
						(v, d) => d,
						(v, c, f) => c.Snapshot.DeleteRepository(v, f),
						(v, c, f) => c.Snapshot.DeleteRepositoryAsync(v, f),
						(_, c, r) => c.Snapshot.DeleteRepository(r),
						(_, c, r) => c.Snapshot.DeleteRepositoryAsync(r))
			}
		}) { }

		[I]
		public async Task CreateRepositoryResponse() => await Assert<CreateRepositoryResponse>(CreateRepositoryStep, r =>
		{
			r.Acknowledged.Should().BeTrue();
		});

		[I]
		public async Task DeleteRepositoryResponse() => await Assert<DeleteRepositoryResponse>(DeleteRepositoryStep, r =>
		{
			r.Acknowledged.Should().BeTrue();
		});

		private static string GetRepositoryPath(string path, string name) => Path.Combine(path, name);
	}
}
