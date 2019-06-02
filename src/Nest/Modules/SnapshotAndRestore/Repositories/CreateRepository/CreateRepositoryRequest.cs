using System;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[MapsApi("snapshot.create_repository.json")]
	[JsonFormatter(typeof(CreateRepositoryFormatter))]
	public partial interface ICreateRepositoryRequest
	{
		ISnapshotRepository Repository { get; set; }
	}

	public partial class CreateRepositoryRequest
	{
		public ISnapshotRepository Repository { get; set; }
	}

	public partial class CreateRepositoryDescriptor
	{
		ISnapshotRepository ICreateRepositoryRequest.Repository { get; set; }

		/// <summary>
		/// 	The shared file system repository ("type": "fs") is using shared file system to store snapshot.
		///  The path specified in the location parameter should point to the same location in the shared
		///  filesystem and be accessible on all data and master nodes.
		/// </summary>
		public CreateRepositoryDescriptor FileSystem(Func<FileSystemRepositoryDescriptor, IFileSystemRepository> selector) =>
			Assign(selector, (a, v) => a.Repository = v?.Invoke(new FileSystemRepositoryDescriptor()));

		/// <summary>
		/// The URL repository ("type": "url") can be used as an alternative read-only way to access data
		/// created by shared file system repository is using shared file system to store snapshot.
		/// </summary>
		public CreateRepositoryDescriptor ReadOnlyUrl(Func<ReadOnlyUrlRepositoryDescriptor, IReadOnlyUrlRepository> selector) =>
			Assign(selector, (a, v) => a.Repository = v?.Invoke(new ReadOnlyUrlRepositoryDescriptor()));

		/// <summary>
		/// Specify an azure storage container to snapshot and restore to. (defaults to a container named elasticsearch-snapshots)
		/// </summary>
		public CreateRepositoryDescriptor Azure(Func<AzureRepositoryDescriptor, IAzureRepository> selector = null) =>
			Assign(selector.InvokeOrDefault(new AzureRepositoryDescriptor()), (a, v) => a.Repository = v);

		/// <summary> Create an snapshot/restore repository that points to an HDFS filesystem </summary>
		public CreateRepositoryDescriptor Hdfs(Func<HdfsRepositoryDescriptor, IHdfsRepository> selector) =>
			Assign(selector, (a, v) => a.Repository = v?.Invoke(new HdfsRepositoryDescriptor()));

		/// <summary> Snapshot and restore to an Amazon S3 bucket </summary>
		public CreateRepositoryDescriptor S3(Func<S3RepositoryDescriptor, IS3Repository> selector) =>
			Assign(selector, (a, v) => a.Repository = v?.Invoke(new S3RepositoryDescriptor()));

		/// <summary> Snapshot and restore to an Amazon S3 bucket </summary>
		public CreateRepositoryDescriptor SourceOnly(Func<SourceOnlyRepositoryDescriptor, ISourceOnlyRepository> selector) =>
			Assign(selector, (a, v) => a.Repository = v?.Invoke(new SourceOnlyRepositoryDescriptor()));

		/// <summary>
		/// Register a custom repository
		/// </summary>
		public CreateRepositoryDescriptor Custom(ISnapshotRepository repository) => Assign(repository, (a, v) => a.Repository = v);
	}
}
