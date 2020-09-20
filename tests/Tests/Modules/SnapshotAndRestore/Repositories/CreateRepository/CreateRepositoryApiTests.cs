// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Transport;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Modules.SnapshotAndRestore.Repositories.CreateRepository
{
	public class CreateAzureRepositoryApiTests
		: ApiTestBase<WritableCluster, CreateRepositoryResponse, ICreateRepositoryRequest, CreateRepositoryDescriptor, CreateRepositoryRequest>
	{
		private static readonly string _name = "repository1";

		public CreateAzureRepositoryApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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

		protected override Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> Fluent => d => d
			.Azure(a => a
				.Settings(s => s
					.Container("foocontainer")
					.BasePath("foopath")
					.Compress()
					.ChunkSize("64mb")
				)
			);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

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

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_snapshot/{_name}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Snapshot.CreateRepository(_name, f),
			(client, f) => client.Snapshot.CreateRepositoryAsync(_name, f),
			(client, r) => client.Snapshot.CreateRepository(r),
			(client, r) => client.Snapshot.CreateRepositoryAsync(r)
		);

		protected override CreateRepositoryDescriptor NewDescriptor() => new CreateRepositoryDescriptor(_name);
	}

	public class CreateHdfsRepositoryApiTests
		: ApiTestBase<WritableCluster, CreateRepositoryResponse, ICreateRepositoryRequest, CreateRepositoryDescriptor, CreateRepositoryRequest>
	{
		private static readonly string _name = "repository1";

		public CreateHdfsRepositoryApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson { get; } = new
		{
			type = "hdfs",
			settings = new
			{
				uri = "foouri",
				path = "some/path",
				load_defaults = true,
				conf_location = "fooconflocation",
				compress = true,
				concurrent_streams = 5,
				chunk_size = "64mb"
			}
		};

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

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

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

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_snapshot/{_name}";


		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Snapshot.CreateRepository(_name, f),
			(client, f) => client.Snapshot.CreateRepositoryAsync(_name, f),
			(client, r) => client.Snapshot.CreateRepository(r),
			(client, r) => client.Snapshot.CreateRepositoryAsync(r)
		);

		protected override CreateRepositoryDescriptor NewDescriptor() => new CreateRepositoryDescriptor(_name);
	}

	public class CreateSourceOnlyRepositoryApiTests
		: ApiTestBase<WritableCluster, CreateRepositoryResponse, ICreateRepositoryRequest, CreateRepositoryDescriptor, CreateRepositoryRequest>
	{
		private static readonly string _name = "repository10";

		public CreateSourceOnlyRepositoryApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			type = "source",
			settings = new
			{
				delegate_type = "fs",
				location = "some/location",
				compress = true,
				concurrent_streams = 5,
				chunk_size = "64mb",
				max_restore_bytes_per_second = "100mb",
				max_snapshot_bytes_per_second = "200mb"
			}
		};

		protected override Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> Fluent => d => d
			.SourceOnly(o => o
				.FileSystem(fs=>fs
					.Settings("some/location", s => s
						.Compress()
						.ConcurrentStreams(5)
						.ChunkSize("64mb")
						.RestoreBytesPerSecondMaximum("100mb")
						.SnapshotBytesPerSecondMaximum("200mb")
					)
				)
			);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override CreateRepositoryRequest Initializer => new CreateRepositoryRequest(_name)
		{
			Repository = new SourceOnlyRepository(new FileSystemRepository(new FileSystemRepositorySettings("some/location")
			{
				Compress = true,
				ConcurrentStreams = 5,
				ChunkSize = "64mb",
				RestoreBytesPerSecondMaximum = "100mb",
				SnapshotBytesPerSecondMaximum = "200mb"
			}))
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_snapshot/{_name}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Snapshot.CreateRepository(_name, f),
			(client, f) => client.Snapshot.CreateRepositoryAsync(_name, f),
			(client, r) => client.Snapshot.CreateRepository(r),
			(client, r) => client.Snapshot.CreateRepositoryAsync(r)
		);

		protected override CreateRepositoryDescriptor NewDescriptor() => new CreateRepositoryDescriptor(_name);
	}


	public class CreateFileSystemRepositoryApiTests
		: ApiTestBase<WritableCluster, CreateRepositoryResponse, ICreateRepositoryRequest, CreateRepositoryDescriptor, CreateRepositoryRequest>
	{
		private static readonly string _name = "repository1";

		public CreateFileSystemRepositoryApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson { get; } = new
		{
			type = "fs",
			settings = new
			{
				location = "some/location",
				compress = true,
				concurrent_streams = 5,
				chunk_size = "64mb",
				max_restore_bytes_per_second = "100mb",
				max_snapshot_bytes_per_second = "200mb"
			}
		};

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

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

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

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_snapshot/{_name}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Snapshot.CreateRepository(_name, f),
			(client, f) => client.Snapshot.CreateRepositoryAsync(_name, f),
			(client, r) => client.Snapshot.CreateRepository(r),
			(client, r) => client.Snapshot.CreateRepositoryAsync(r)
		);

		protected override CreateRepositoryDescriptor NewDescriptor() => new CreateRepositoryDescriptor(_name);
	}

	public class CreateReadOnlyUrlRepositoryApiTests
		: ApiTestBase<WritableCluster, CreateRepositoryResponse, ICreateRepositoryRequest, CreateRepositoryDescriptor, CreateRepositoryRequest>
	{
		private static readonly string _name = "repository1";

		public CreateReadOnlyUrlRepositoryApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson { get; } = new
		{
			type = "url",
			settings = new
			{
				location = "http://some/location",
				concurrent_streams = 5
			}
		};

		protected override Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> Fluent => d => d
			.ReadOnlyUrl(u => u
				.Settings("http://some/location", s => s
					.ConcurrentStreams(5)
				)
			);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

		protected override CreateRepositoryRequest Initializer => new CreateRepositoryRequest(_name)
		{
			Repository = new ReadOnlyUrlRepository(new ReadOnlyUrlRepositorySettings("http://some/location")
			{
				ConcurrentStreams = 5,
			})
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_snapshot/{_name}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Snapshot.CreateRepository(_name, f),
			(client, f) => client.Snapshot.CreateRepositoryAsync(_name, f),
			(client, r) => client.Snapshot.CreateRepository(r),
			(client, r) => client.Snapshot.CreateRepositoryAsync(r)
		);

		protected override CreateRepositoryDescriptor NewDescriptor() => new CreateRepositoryDescriptor(_name);
	}

	public class CreateS3RepositoryApiTests
		: ApiTestBase<WritableCluster, CreateRepositoryResponse, ICreateRepositoryRequest, CreateRepositoryDescriptor, CreateRepositoryRequest>
	{
		private static readonly string _name = "repository1";

		public CreateS3RepositoryApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson { get; } = new
		{
			type = "s3",
			settings = new
			{
				bucket = "foobucket",
				client = "default",
				base_path = "some/path",
				compress = true,
				chunk_size = "64mb",
				server_side_encryption = true,
				buffer_size = "100mb",
				canned_acl = "authenticated-read",
				storage_class = "standard",
				max_restore_bytes_per_sec = "40mb",
				max_snapshot_bytes_per_sec = "40mb",
			}
		};

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
					.MaxRestoreBytesPerSecond("40mb")
					.MaxSnapshotBytesPerSecond("40mb")
				)
			);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;

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
				StorageClass = "standard",
				MaxRestoreBytesPerSecond = "40mb",
				MaxSnapshotBytesPerSecond = "40mb"
			})
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_snapshot/{_name}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Snapshot.CreateRepository(_name, f),
			(client, f) => client.Snapshot.CreateRepositoryAsync(_name, f),
			(client, r) => client.Snapshot.CreateRepository(r),
			(client, r) => client.Snapshot.CreateRepositoryAsync(r)
		);

		protected override CreateRepositoryDescriptor NewDescriptor() => new CreateRepositoryDescriptor(_name);
	}
}
