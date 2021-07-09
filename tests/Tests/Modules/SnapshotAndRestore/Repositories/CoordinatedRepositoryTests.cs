// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using System.Linq;
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
		private const string GetAfterUpdateRepositoryStep = nameof(GetAfterUpdateRepositoryStep);
		private const string GetRepositoryStep = nameof(GetRepositoryStep);
		private const string UpdateRepositoryStep = nameof(UpdateRepositoryStep);

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
				GetRepositoryStep, u => u
					.Calls<GetRepositoryDescriptor, GetRepositoryRequest, IGetRepositoryRequest, GetRepositoryResponse>(
						v => new GetRepositoryRequest(v),
						(v, d) => d.RepositoryName(v),
						(v, c, f) => c.Snapshot.GetRepository(f),
						(v, c, f) => c.Snapshot.GetRepositoryAsync(f),
						(_, c, r) => c.Snapshot.GetRepository(r),
						(_, c, r) => c.Snapshot.GetRepositoryAsync(r))
			},
			{
				AnalyzeRepositoryStep, ">=7.14.0", u => u
					.Calls<AnalyzeRepositoryDescriptor, AnalyzeRepositoryRequest, IAnalyzeRepositoryRequest, AnalyzeRepositoryResponse>(
						v => new AnalyzeRepositoryRequest(v) { Detailed = true },
						(v, d) => d.Detailed(),
						(v, c, f) => c.Snapshot.AnalyzeRepository(v, f),
						(v, c, f) => c.Snapshot.AnalyzeRepositoryAsync(v, f),
						(_, c, r) => c.Snapshot.AnalyzeRepository(r),
						(_, c, r) => c.Snapshot.AnalyzeRepositoryAsync(r))
			},
			{
				UpdateRepositoryStep, u => u
					.Calls<CreateRepositoryDescriptor, CreateRepositoryRequest, ICreateRepositoryRequest, CreateRepositoryResponse>(
						v => new CreateRepositoryRequest(v)
						{
							Repository = new FileSystemRepository(
								new FileSystemRepositorySettings(GetRepositoryPath(cluster.FileSystem.RepositoryPath, v))
								{
									ChunkSize = "64mb", Compress = true, ConcurrentStreams = 5, ReadOnly = true
								}
							)
						},
						(v, d) => d.FileSystem(fs => fs
							.Settings(GetRepositoryPath(cluster.FileSystem.RepositoryPath, v), s => s
								.ChunkSize("64mb")
								.Compress()
								.ConcurrentStreams(5)
								.ReadOnly()
							)
						),
						(v, c, f) => c.Snapshot.CreateRepository(v, f),
						(v, c, f) => c.Snapshot.CreateRepositoryAsync(v, f),
						(_, c, r) => c.Snapshot.CreateRepository(r),
						(_, c, r) => c.Snapshot.CreateRepositoryAsync(r))
			},
			{
				GetAfterUpdateRepositoryStep, u => u
					.Calls<GetRepositoryDescriptor, GetRepositoryRequest, IGetRepositoryRequest, GetRepositoryResponse>(
						v => new GetRepositoryRequest(v),
						(v, d) => d.RepositoryName(v),
						(v, c, f) => c.Snapshot.GetRepository(f),
						(v, c, f) => c.Snapshot.GetRepositoryAsync(f),
						(_, c, r) => c.Snapshot.GetRepository(r),
						(_, c, r) => c.Snapshot.GetRepositoryAsync(r))
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
		public async Task GetRepositoryResponse() => await Assert<GetRepositoryResponse>(GetRepositoryStep, r =>
		{
			r.Repositories.Should().NotBeNull().And.HaveCount(1);
			var name = r.Repositories.Keys.First();
			var repository = r.FileSystem(name);
			repository.Should().NotBeNull();
			repository.Type.Should().Be("fs");
			repository.Settings.Should().NotBeNull();
			repository.Settings.ChunkSize.Should().Be("64mb");
			repository.Settings.Compress.Should().BeTrue();
			repository.Settings.ReadOnly.Should().BeNull();
		});

		[I]
		public async Task AnalyzeRepositoryResponse() => await Assert<AnalyzeRepositoryResponse>(AnalyzeRepositoryStep, r =>
		{
			r.IsValid.Should().BeTrue();
			r.CoordinatingNode?.Id.Should().NotBeNullOrEmpty();
			r.CoordinatingNode?.Name.Should().NotBeNullOrEmpty();
			r.Repository.Should().NotBeNullOrEmpty();
			r.BlobCount.Should().BeGreaterThan(0);
			r.Concurrency.Should().BeGreaterThan(0);
			r.ReadNodeCount.Should().BeGreaterThan(0);
			r.EarlyReadNodeCount.Should().BeGreaterOrEqualTo(0);
			r.MaxBlobSize.Should().NotBeNullOrEmpty();
			r.MaxBlobSizeBytes.Should().BeGreaterThan(0);
			r.MaxTotalDataSize.Should().NotBeNullOrEmpty();
			r.MaxTotalDataSizeBytes.Should().BeGreaterThan(0);
			r.Seed.Should().BeGreaterThan(0);
			r.RareActionProbability.Should().BeGreaterThan(0);
			r.BlobPath.Should().NotBeNullOrEmpty();
			r.IssuesDetected.Should().HaveCountGreaterOrEqualTo(0);
			r.ListingElapsed.Should().NotBeNullOrEmpty();
			r.ListingElapsedNanos.Should().BeGreaterThan(0);
			r.DeleteElapsed.Should().NotBeNullOrEmpty();
			r.DeleteElapsedNanos.Should().BeGreaterThan(0);

			// summary
			r.Summary.Should().NotBeNull();
			var s = r.Summary;
			s.Write.Count.Should().BeGreaterThan(0);
			s.Write.TotalSizeBytes.Should().BeGreaterThan(0);
			s.Write.TotalThrottledNanos.Should().BeGreaterThan(0);
			s.Write.TotalElapsedNanos.Should().BeGreaterThan(0);
			s.Write.TotalSize.Should().NotBeNullOrEmpty();
			s.Write.TotalThrottled.Should().NotBeNullOrEmpty();
			s.Write.TotalElapsed.Should().NotBeNullOrEmpty();
			s.Read.Count.Should().BeGreaterThan(0);
			s.Read.TotalSizeBytes.Should().BeGreaterThan(0);
			s.Read.TotalThrottledNanos.Should().BeGreaterThan(0);
			s.Read.TotalElapsedNanos.Should().BeGreaterThan(0);
			s.Read.TotalSize.Should().NotBeNullOrEmpty();
			s.Read.TotalThrottled.Should().NotBeNullOrEmpty();
			s.Read.TotalElapsed.Should().NotBeNullOrEmpty();

			// details
			r.Details.Should().HaveCountGreaterThan(1);
			var d = r.Details.First();
			d.Blob.Name.Should().NotBeNullOrEmpty();
			d.Blob.Size.Should().NotBeNullOrEmpty();
			d.Blob.SizeBytes.Should().BeGreaterOrEqualTo(0);
			// Hard to check the remainder of the blog properties as we can't be sure what will be returned from the server
			d.WriterNode.Id.Should().NotBeNullOrEmpty();
			d.WriterNode.Name.Should().NotBeNullOrEmpty();
			d.WriteElapsed.Should().NotBeNullOrEmpty();
			d.WriteElapsedNanos.Should().BeGreaterThan(0);
			d.WriteThrottled.Should().NotBeNullOrEmpty();
			d.WriteThrottledNanos.Should().BeGreaterOrEqualTo(0);
			d.Reads.Should().HaveCountGreaterOrEqualTo(1);
			var reads = d.Reads.First();
			reads.NodeIdentity.Id.Should().NotBeNullOrEmpty();
			reads.NodeIdentity.Name.Should().NotBeNullOrEmpty();
			reads.FirstByteTime.Should().NotBeNullOrEmpty();
			reads.FirstByteTimeNanos.Should().BeGreaterThan(0);
			reads.Elapsed.Should().NotBeNullOrEmpty();
			reads.ElapsedNanos.Should().BeGreaterThan(0);
			reads.Throttled.Should().NotBeNullOrEmpty();
			reads.ThrottledNanos.Should().BeGreaterOrEqualTo(0);
		});

		[I]
		public async Task GetAfterUpdateRepositoryResponse() => await Assert<GetRepositoryResponse>(GetAfterUpdateRepositoryStep, r =>
		{
			r.Repositories.Should().NotBeNull().And.HaveCount(1);
			var name = r.Repositories.Keys.First();
			var repository = r.FileSystem(name);
			repository.Should().NotBeNull();
			repository.Type.Should().Be("fs");
			repository.Settings.Should().NotBeNull();
			repository.Settings.ChunkSize.Should().Be("64mb");
			repository.Settings.Compress.Should().BeTrue();
			repository.Settings.ConcurrentStreams.Should().Be(5);
			repository.Settings.ReadOnly.Should().BeTrue();
		});

		[I]
		public async Task DeleteRepositoryResponse() => await Assert<DeleteRepositoryResponse>(DeleteRepositoryStep, r =>
		{
			r.Acknowledged.Should().BeTrue();
		});

		private static string GetRepositoryPath(string path, string name) => Path.Combine(path, name);
	}
}
