using System.IO;
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Modules.SnapshotAndRestore.Repositories
{
	public class RepositoryCrudTests
		: CrudTestBase<IntrusiveOperationCluster, ICreateRepositoryResponse, IGetRepositoryResponse, ICreateRepositoryResponse,
			IDeleteRepositoryResponse>
	{
		private readonly string _rootRepositoryPath;

		public RepositoryCrudTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) =>
			_rootRepositoryPath = cluster.FileSystem.RepositoryPath;

		protected override LazyResponses Create() =>
			Calls<CreateRepositoryDescriptor, CreateRepositoryRequest, ICreateRepositoryRequest, ICreateRepositoryResponse>(
				CreateInitializer,
				CreateFluent,
				(s, c, f) => c.CreateRepository(s, f),
				(s, c, f) => c.CreateRepositoryAsync(s, f),
				(s, c, r) => c.CreateRepository(r),
				(s, c, r) => c.CreateRepositoryAsync(r)
			);

		private string GetRepositoryPath(string name) => Path.Combine(_rootRepositoryPath, name);

		protected CreateRepositoryRequest CreateInitializer(string name) =>
			new CreateRepositoryRequest(name)
			{
				Repository = new FileSystemRepository(
					new FileSystemRepositorySettings(GetRepositoryPath(name))
					{
						ChunkSize = "64mb",
						Compress = true
					}
				)
			};

		protected ICreateRepositoryRequest CreateFluent(string name, CreateRepositoryDescriptor d) => d
			.FileSystem(fs => fs
				.Settings(GetRepositoryPath(name), s => s
					.ChunkSize("64mb")
					.Compress()
				)
			);

		protected override LazyResponses Read() =>
			Calls<GetRepositoryDescriptor, GetRepositoryRequest, IGetRepositoryRequest, IGetRepositoryResponse>(
				ReadInitializer,
				ReadFluent,
				(s, c, f) => c.GetRepository(f),
				(s, c, f) => c.GetRepositoryAsync(f),
				(s, c, r) => c.GetRepository(r),
				(s, c, r) => c.GetRepositoryAsync(r)
			);

		protected GetRepositoryRequest ReadInitializer(string name) => new GetRepositoryRequest(name);

		protected IGetRepositoryRequest ReadFluent(string name, GetRepositoryDescriptor d) => d
			.RepositoryName(name);

		protected override LazyResponses Update() =>
			Calls<CreateRepositoryDescriptor, CreateRepositoryRequest, ICreateRepositoryRequest, ICreateRepositoryResponse>(
				UpdateInitializer,
				UpdateFluent,
				(s, c, f) => c.CreateRepository(s, f),
				(s, c, f) => c.CreateRepositoryAsync(s, f),
				(s, c, r) => c.CreateRepository(r),
				(s, c, r) => c.CreateRepositoryAsync(r)
			);

		protected CreateRepositoryRequest UpdateInitializer(string name) => new CreateRepositoryRequest(name)
		{
			Repository = new FileSystemRepository(new FileSystemRepositorySettings(GetRepositoryPath(name))
				{
					ChunkSize = "64mb",
					Compress = true,
					ConcurrentStreams = 5
				}
			)
		};

		protected ICreateRepositoryRequest UpdateFluent(string name, CreateRepositoryDescriptor d) => d
			.FileSystem(fs => fs
				.Settings(GetRepositoryPath(name), s => s
					.ChunkSize("64mb")
					.Compress()
					.ConcurrentStreams(5)
				)
			);

		protected override LazyResponses Delete() =>
			Calls<DeleteRepositoryDescriptor, DeleteRepositoryRequest, IDeleteRepositoryRequest, IDeleteRepositoryResponse>(
				DeleteInitializer,
				DeleteFluent,
				(s, c, f) => c.DeleteRepository(s),
				(s, c, f) => c.DeleteRepositoryAsync(s),
				(s, c, r) => c.DeleteRepository(r),
				(s, c, r) => c.DeleteRepositoryAsync(r)
			);

		protected DeleteRepositoryRequest DeleteInitializer(string name) => new DeleteRepositoryRequest(name);

		protected IDeleteRepositoryRequest DeleteFluent(string name, DeleteRepositoryDescriptor d) => null;

		protected override void ExpectAfterCreate(IGetRepositoryResponse response)
		{
			response.Repositories.Should().NotBeNull().And.HaveCount(1);
			var name = response.Repositories.Keys.First();
			var repository = response.FileSystem(name);
			repository.Should().NotBeNull();
			repository.Type.Should().Be("fs");
			repository.Settings.Should().NotBeNull();
			repository.Settings.ChunkSize.Should().Be("64mb");
			repository.Settings.Compress.Should().BeTrue();
		}

		protected override void ExpectAfterUpdate(IGetRepositoryResponse response)
		{
			response.Repositories.Should().NotBeNull().And.HaveCount(1);
			var name = response.Repositories.Keys.First();
			var repository = response.FileSystem(name);
			repository.Should().NotBeNull();
			repository.Type.Should().Be("fs");
			repository.Settings.Should().NotBeNull();
			repository.Settings.ChunkSize.Should().Be("64mb");
			repository.Settings.Compress.Should().BeTrue();
			repository.Settings.ConcurrentStreams.Should().Be(5);
		}
	}
}
