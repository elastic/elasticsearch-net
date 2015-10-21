using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{

	[JsonConverter(typeof(CreateRepositoryJsonConverter))]
	public partial interface ICreateRepositoryRequest 
	{
		IRepository Repository { get; set; }
	}

	public partial class CreateRepositoryRequest 
	{
		public IRepository Repository { get; set; }
	}

	[DescriptorFor("SnapshotCreateRepository")]
	public partial class CreateRepositoryDescriptor 
	{
		IRepository ICreateRepositoryRequest.Repository { get; set; }

		/// <summary>
		///	The shared file system repository ("type": "fs") is using shared file system to store snapshot. 
		/// The path specified in the location parameter should point to the same location in the shared 
		/// filesystem and be accessible on all data and master nodes. 
		/// </summary>
		/// <param name="location"></param>
		/// <param name="options"></param>
		public CreateRepositoryDescriptor FileSystem(string location, Func<FileSystemRepositoryDescriptor, IRepository> options = null) =>
			Assign(a => a.Repository = options.InvokeOrDefault(new FileSystemRepositoryDescriptor().Location(location)));
		
		/// <summary>
		/// The URL repository ("type": "url") can be used as an alternative read-only way to access data 
		/// created by shared file system repository is using shared file system to store snapshot. 
		/// </summary>
		/// <param name="location"></param>
		/// <param name="options"></param>
		public CreateRepositoryDescriptor ReadOnlyUrl(string location, Func<ReadOnlyUrlRepositoryDescriptor, IRepository> options = null) =>
			Assign(a => a.Repository = options.InvokeOrDefault(new ReadOnlyUrlRepositoryDescriptor().Location(location)));
	
		/// <summary>
		/// Specify an azure storage container to snapshot and restore to. (defaults to a container named elasticsearch-snapshots)
		/// </summary>
		public CreateRepositoryDescriptor Azure(Func<AzureRepositoryDescriptor, IRepository> options = null) =>
			Assign(a => a.Repository = options.InvokeOrDefault(new AzureRepositoryDescriptor()));

		/// <summary>
		/// Create an snapshot/restore repository that points to an HDFS filesystem
		/// </summary>
		/// <param name="path"></param>
		/// <param name="options"></param>
		public CreateRepositoryDescriptor Hdfs(string path, Func<HdfsRepositoryDescriptor, IRepository> options = null) =>
			Assign(a => a.Repository = options.InvokeOrDefault(new HdfsRepositoryDescriptor().Path(path)));

		/// <summary>
		/// Register a custom repository
		/// </summary>
		public CreateRepositoryDescriptor Custom(IRepository repository) => Assign(a => a.Repository = repository);
	}
}
