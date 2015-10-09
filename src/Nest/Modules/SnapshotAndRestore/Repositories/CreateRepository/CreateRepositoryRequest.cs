using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
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
		public CreateRepositoryDescriptor FileSystem(string location, Action<FileSystemRepositoryDescriptor> options = null)
		{
			location.ThrowIfNullOrEmpty("location");
			var repos = new FileSystemRepositoryDescriptor().Location(location);
			if (options != null) options(repos);
			Self.Repository = repos;
			return this;
		}
		
		/// <summary>
		/// The URL repository ("type": "url") can be used as an alternative read-only way to access data 
		/// created by shared file system repository is using shared file system to store snapshot. 
		/// </summary>
		/// <param name="location"></param>
		/// <param name="options"></param>
		public CreateRepositoryDescriptor ReadOnlyUrl(string location, Action<ReadOnlyUrlRepositoryDescriptor> options = null)
		{
			location.ThrowIfNullOrEmpty("location");
			var repos = new ReadOnlyUrlRepositoryDescriptor().Location(location);
			if (options != null) options(repos);
			Self.Repository = repos;
			return this;
		}
	
		/// <summary>
		/// Specify an azure storage container to snapshot and restore to. (defaults to a container named elasticsearch-snapshots)
		/// </summary>
		public CreateRepositoryDescriptor Azure(Action<AzureRepositoryDescriptor> options = null)
		{
			var repos = new AzureRepositoryDescriptor();
			if (options != null) options(repos);
			Self.Repository = repos;
			return this;
		}

		/// <summary>
		/// Create an snapshot/restore repository that points to an HDFS filesystem
		/// </summary>
		/// <param name="path"></param>
		/// <param name="options"></param>
		public CreateRepositoryDescriptor Hdfs(string path, Action<HdfsRepositoryDescriptor> options = null)
		{
			path.ThrowIfNullOrEmpty(path);
			var repos = new HdfsRepositoryDescriptor().Path(path);
			if (options != null) options(repos);
			Self.Repository = repos;
			return this;
		}
		
		/// <summary>
		/// Register a custom repository
		/// </summary>
		public CreateRepositoryDescriptor Custom(IRepository repository)
		{
			Self.Repository = repository;
			return this;
		}
	}
}
