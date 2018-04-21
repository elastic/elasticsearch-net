using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Modules.SnapshotAndRestore.Repositories.CreateRepository
{
	public class CreateAzureRepositoryApiTests : ApiTestBase<WritableCluster, ICreateRepositoryResponse, ICreateRepositoryRequest, CreateRepositoryDescriptor, CreateRepositoryRequest>
	{
		public CreateAzureRepositoryApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static readonly string _name = "repository1";

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CreateRepository(_name, f),
			fluentAsync: (client, f) => client.CreateRepositoryAsync(_name, f),
			request: (client, r) => client.CreateRepository(r),
			requestAsync: (client, r) => client.CreateRepositoryAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"/_snapshot/{_name}";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new
		{
			type = "azure",
			settings = new
			{
				container = "foocontainer",
				base_path = "foopath",
				compress = true,
				chunk_size = "64mb"
			}
		};

		protected override CreateRepositoryDescriptor NewDescriptor() => new CreateRepositoryDescriptor(_name);

		protected override Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> Fluent => d => d
			.Azure(a => a
				.Settings(s => s
					.Container("foocontainer")
					.BasePath("foopath")
					.Compress()
					.ChunkSize("64mb")
				)
			);

		protected override CreateRepositoryRequest Initializer => new CreateRepositoryRequest(_name)
		{
			Repository = new AzureRepository()
			{
				Settings = new AzureRepositorySettings
				{
					Container = "foocontainer",
					BasePath = "foopath",
					Compress = true,
					ChunkSize = "64mb"
				}
			}
		};
	}

	public class CreateHdfsRepositoryApiTests : ApiTestBase<WritableCluster, ICreateRepositoryResponse, ICreateRepositoryRequest, CreateRepositoryDescriptor, CreateRepositoryRequest>
	{
		public CreateHdfsRepositoryApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static readonly string _name = "repository1";


		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CreateRepository(_name, f),
			fluentAsync: (client, f) => client.CreateRepositoryAsync(_name, f),
			request: (client, r) => client.CreateRepository(r),
			requestAsync: (client, r) => client.CreateRepositoryAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"/_snapshot/{_name}";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new
		{
			type = "hdfs",
			settings = new {
				uri = "foouri",
				path = "some/path",
				load_defaults = true,
				conf_location = "fooconflocation",
				compress = true,
				concurrent_streams = 5,
				chunk_size = "64mb"
			}
		};

		protected override CreateRepositoryDescriptor NewDescriptor() => new CreateRepositoryDescriptor(_name);

		protected override Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> Fluent => d => d
			.Hdfs(h => h
				.Settings("some/path", s => s
					.Uri("foouri")
					.LoadDefaults()
					.ConfigurationLocation("fooconflocation")
					.Compress()
					.ConcurrentStreams(5)
					.ChunkSize("64mb")
				)
			);

		protected override CreateRepositoryRequest Initializer => new CreateRepositoryRequest(_name)
		{
			Repository = new HdfsRepository(new HdfsRepositorySettings("some/path")
			{
				Uri = "foouri",
				LoadDefaults = true,
				ConfigurationLocation = "fooconflocation",
				Compress = true,
				ConcurrentStreams = 5,
				ChunkSize = "64mb"
			})
		};
	}

	public class CreateFileSystemRepositoryApiTests
		: ApiTestBase<WritableCluster, ICreateRepositoryResponse, ICreateRepositoryRequest, CreateRepositoryDescriptor, CreateRepositoryRequest>
	{
		public CreateFileSystemRepositoryApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static readonly string _name = "repository1";

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CreateRepository(_name, f),
			fluentAsync: (client, f) => client.CreateRepositoryAsync(_name, f),
			request: (client, r) => client.CreateRepository(r),
			requestAsync: (client, r) => client.CreateRepositoryAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"/_snapshot/{_name}";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new
		{
			type = "fs",
			settings = new {
				location = "some/location",
				compress = true,
				concurrent_streams = 5,
				chunk_size = "64mb",
				max_restore_bytes_per_second = "100mb",
				max_snapshot_bytes_per_second = "200mb"
			}
		};

		protected override CreateRepositoryDescriptor NewDescriptor() => new CreateRepositoryDescriptor(_name);

		protected override Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> Fluent => d => d
			.FileSystem(fs => fs
				.Settings("some/location", s => s
					.Compress()
					.ConcurrentStreams(5)
					.ChunkSize("64mb")
					.RestoreBytesPerSecondMaximum("100mb")
					.SnapshotBytesPerSecondMaximum("200mb")
				)
			);

		protected override CreateRepositoryRequest Initializer => new CreateRepositoryRequest(_name)
		{
			Repository = new FileSystemRepository(new FileSystemRepositorySettings("some/location")
			{
				Compress = true,
				ConcurrentStreams = 5,
				ChunkSize = "64mb",
				RestoreBytesPerSecondMaximum = "100mb",
				SnapshotBytesPerSecondMaximum = "200mb"
			})
		};
	}

	public class CreateReadOnlyUrlRepositoryApiTests : ApiTestBase<WritableCluster, ICreateRepositoryResponse, ICreateRepositoryRequest, CreateRepositoryDescriptor, CreateRepositoryRequest>
	{
		public CreateReadOnlyUrlRepositoryApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static readonly string _name = "repository1";

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CreateRepository(_name, f),
			fluentAsync: (client, f) => client.CreateRepositoryAsync(_name, f),
			request: (client, r) => client.CreateRepository(r),
			requestAsync: (client, r) => client.CreateRepositoryAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"/_snapshot/{_name}";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new
		{
			type = "url",
			settings = new {
				location = "http://some/location",
				concurrent_streams = 5
			}
		};

		protected override CreateRepositoryDescriptor NewDescriptor() => new CreateRepositoryDescriptor(_name);

		protected override Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> Fluent => d => d
			.ReadOnlyUrl(u => u
				.Settings("http://some/location", s => s
					.ConcurrentStreams(5)
				)
			);

		protected override CreateRepositoryRequest Initializer => new CreateRepositoryRequest(_name)
		{
			Repository = new ReadOnlyUrlRepository(new ReadOnlyUrlRepositorySettings("http://some/location")
			{
				ConcurrentStreams = 5,
			})
		};
	}

	public class CreateS3RepositoryApiTests : ApiTestBase<WritableCluster, ICreateRepositoryResponse, ICreateRepositoryRequest, CreateRepositoryDescriptor, CreateRepositoryRequest>
	{
		public CreateS3RepositoryApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static readonly string _name = "repository1";

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CreateRepository(_name, f),
			fluentAsync: (client, f) => client.CreateRepositoryAsync(_name, f),
			request: (client, r) => client.CreateRepository(r),
			requestAsync: (client, r) => client.CreateRepositoryAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"/_snapshot/{_name}";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new
		{
			type = "s3",
			settings = new {
				bucket = "foobucket",
				client = "default",
				base_path = "some/path",
				compress = true,
				chunk_size = "64mb",
				server_side_encryption = true,
				buffer_size = "100mb",
				canned_acl = "authenticated-read",
				storage_class = "standard"
			}
		};

		protected override CreateRepositoryDescriptor NewDescriptor() => new CreateRepositoryDescriptor(_name);

		protected override Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> Fluent => d => d
			.S3(fs => fs
				.Settings("foobucket", s => s
					.BasePath("some/path")
					.Client("default")
					.Compress()
					.ChunkSize("64mb")
					.ServerSideEncryption()
					.BufferSize("100mb")
					.CannedAcl("authenticated-read")
					.StorageClass("standard")
				)
			);

		protected override CreateRepositoryRequest Initializer => new CreateRepositoryRequest(_name)
		{
			Repository = new S3Repository(new S3RepositorySettings("foobucket")
			{
				BasePath = "some/path",
				Client = "default",
				Compress = true,
				ChunkSize = "64mb",
				ServerSideEncryption = true,
				BufferSize = "100mb",
				CannedAcl = "authenticated-read",
				StorageClass = "standard"
			})
		};
	}
}
