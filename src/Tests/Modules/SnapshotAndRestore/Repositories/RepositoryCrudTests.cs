using FluentAssertions;
using Nest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Modules.SnapshotAndRestore.Repositories
{
	[Collection(IntegrationContext.Indexing)]
	public class RepositoryCrudTests
		: CrudTestBase<IAcknowledgedResponse, IGetRepositoryResponse, IAcknowledgedResponse, IAcknowledgedResponse>
	{
		public RepositoryCrudTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses Create() => Calls<CreateRepositoryDescriptor, CreateRepositoryRequest, ICreateRepositoryRequest, IAcknowledgedResponse>(
			CreateInitializer,
			CreateFluent,
			fluent: (s, c, f) => c.CreateRepository(s, f),
			fluentAsync: (s, c, f) => c.CreateRepositoryAsync(s, f),
			request: (s, c, r) => c.CreateRepository(r),
			requestAsync: (s, c, r) => c.CreateRepositoryAsync(r)
        );

		private string Location(string name) => $"C:/nest-repository-{name}";

		protected CreateRepositoryRequest CreateInitializer(string name) =>
			new CreateRepositoryRequest(name)
			{
				Repository = new FileSystemRepository(Location(name))
            };

		protected ICreateRepositoryRequest CreateFluent(string name, CreateRepositoryDescriptor d) => d
			.FileSystem(Location(name));

		protected override LazyResponses Read() => Calls<GetRepositoryDescriptor, GetRepositoryRequest, IGetRepositoryRequest, IGetRepositoryResponse>(
			ReadInitializer,
			ReadFluent,
			fluent: (s, c, f) => c.GetRepository(f),
			fluentAsync: (s, c, f) => c.GetRepositoryAsync(f),
			request: (s, c, r) => c.GetRepository(r),
			requestAsync: (s, c, r) => c.GetRepositoryAsync(r)
		);

		protected GetRepositoryRequest ReadInitializer(string name) => new GetRepositoryRequest(name);

		protected IGetRepositoryRequest ReadFluent(string name, GetRepositoryDescriptor d) => d
			.RepositoryName(name);

		protected override LazyResponses Update() => Calls<CreateRepositoryDescriptor, CreateRepositoryRequest, ICreateRepositoryRequest, IAcknowledgedResponse>(
			UpdateInitializer,
			UpdateFluent,
			fluent: (s, c, f) => c.CreateRepository(s, f),
			fluentAsync: (s, c, f) => c.CreateRepositoryAsync(s, f),
			request: (s, c, r) => c.CreateRepository(r),
			requestAsync: (s, c, r) => c.CreateRepositoryAsync(r)
        );

		protected CreateRepositoryRequest UpdateInitializer(string name) => new CreateRepositoryRequest(name)
		{
			Repository = new FileSystemRepository(Location(name))
			{
				ChunkSize = "64mb"
			}
		};

		protected ICreateRepositoryRequest UpdateFluent(string name, CreateRepositoryDescriptor d) => d
			.FileSystem(Location(name), fs => fs
				.ChunkSize("64mb")
			);

		protected override LazyResponses Delete() => Calls<DeleteRepositoryDescriptor, DeleteRepositoryRequest, IDeleteRepositoryRequest, IAcknowledgedResponse>(
			DeleteInitializer,
			DeleteFluent,
			fluent: (s, c, f) => c.DeleteRepository(s),
			fluentAsync: (s, c, f) => c.DeleteRepositoryAsync(s),
			request: (s, c, r) => c.DeleteRepository(r),
			requestAsync: (s, c, r) => c.DeleteRepositoryAsync(r)
        );

		protected DeleteRepositoryRequest DeleteInitializer(string name) => new DeleteRepositoryRequest(name);

		protected IDeleteRepositoryRequest DeleteFluent(string name, DeleteRepositoryDescriptor d) => null;
	}
}
