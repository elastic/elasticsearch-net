using System;
using Newtonsoft.Json;

namespace Nest
{

	[JsonConverter(typeof(CreateRepositoryJsonConverter))]
	public partial interface ICreateRepositoryRequest 
	{
		ISnapshotRepository Repository { get; set; }
	}

	public partial class CreateRepositoryRequest 
	{
		public ISnapshotRepository Repository { get; set; }
	}

	[DescriptorFor("SnapshotCreateRepository")]
	public partial class CreateRepositoryDescriptor
	{
		ISnapshotRepository ICreateRepositoryRequest.Repository { get; set; }

		/// <summary>
		///	The shared file system repository ("type": "fs") is using shared file system to store snapshot. 
		/// The path specified in the location parameter should point to the same location in the shared 
		/// filesystem and be accessible on all data and master nodes. 
		/// </summary>
		/// <param name="location"></param>
		/// <param name="selector"></param>
		public CreateRepositoryDescriptor FileSystem(Func<FileSystemRepositoryDescriptor, IFileSystemRepository> selector) =>
			Assign(a => a.Repository = selector?.Invoke(new FileSystemRepositoryDescriptor()));

		/// <summary>
		/// The URL repository ("type": "url") can be used as an alternative read-only way to access data 
		/// created by shared file system repository is using shared file system to store snapshot. 
		/// </summary>
		/// <param name="location"></param>
		/// <param name="selector"></param>
		public CreateRepositoryDescriptor ReadOnlyUrl(Func<ReadOnlyUrlRepositoryDescriptor, IReadOnlyUrlRepository> selector) =>
			Assign(a => a.Repository = selector?.Invoke(new ReadOnlyUrlRepositoryDescriptor()));
	
		/// <summary>
		/// Specify an azure storage container to snapshot and restore to. (defaults to a container named elasticsearch-snapshots)
		/// </summary>
		public CreateRepositoryDescriptor Azure(Func<AzureRepositoryDescriptor, IAzureRepository> selector = null) =>
			Assign(a => a.Repository = selector.InvokeOrDefault(new AzureRepositoryDescriptor()));

		/// <summary>
		/// Create an snapshot/restore repository that points to an HDFS filesystem
		/// </summary>
		/// <param name="path"></param>
		/// <param name="selector"></param>
		public CreateRepositoryDescriptor Hdfs(Func<HdfsRepositoryDescriptor, IHdfsRepository> selector) =>
			Assign(a => a.Repository = selector?.Invoke(new HdfsRepositoryDescriptor()));

		/// <summary>
		/// Snapshot and restore to an Amazon S3 bucket
		/// </summary>
		/// <param name="bucket"></param>
		/// <param name="selector"></param>
		/// <returns></returns>
		public CreateRepositoryDescriptor S3(Func<S3RepositoryDescriptor, IS3Repository> selector) =>
			Assign(a => a.Repository = selector?.Invoke(new S3RepositoryDescriptor()));

		/// <summary>
		/// Register a custom repository
		/// </summary>
		public CreateRepositoryDescriptor Custom(ISnapshotRepository repository) => Assign(a => a.Repository = repository);
	}
}
