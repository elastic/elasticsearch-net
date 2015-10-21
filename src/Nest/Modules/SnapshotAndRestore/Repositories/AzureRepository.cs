using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Nest
{
	public class AzureRepository : IRepository
	{
		string IRepository.Type { get { return "azure"; } }
		public IDictionary<string, object> Settings { get; set; } = new Dictionary<string, object>();
	}

	public class AzureRepositoryDescriptor : IRepository
	{
		string IRepository.Type { get { return "azure"; } }
		IDictionary<string, object> IRepository.Settings { get; set; }

		private IRepository Self => this;

		public AzureRepositoryDescriptor()
		{
			Self.Settings = new Dictionary<string, object>();
		}
		/// <summary>
		/// Container name. Defaults to elasticsearch-snapshots
		/// </summary>
		/// <param name="bucket"></param>
		public AzureRepositoryDescriptor Container(string bucket)
		{
			Self.Settings["bucket"] = bucket;
			return this;
		}
		/// <summary>
		///Specifies the path within container to repository data. Defaults to empty (root directory).
		/// </summary>
		/// <param name="basePath"></param>
		/// <returns></returns>
		public AzureRepositoryDescriptor BasePath(string basePath)
		{
			Self.Settings["base_path"] = basePath;
			return this;
		}
		/// <summary>
		/// When set to true metadata files are stored in compressed format. This setting doesn't 
		/// affect index files that are already compressed by default. Defaults to false.
		/// </summary>
		/// <param name="compress"></param>
		public AzureRepositoryDescriptor Compress(bool compress = true)
		{
			Self.Settings["compress"] = compress;
			return this;
		}
		/// <summary>
		/// Throttles the number of streams (per node) preforming snapshot operation. Defaults to 5
		/// </summary>
		/// <param name="concurrentStreams"></param>
		public AzureRepositoryDescriptor ConcurrentStreams(int concurrentStreams)
		{
			Self.Settings["concurrent_streams"] = concurrentStreams;
			return this;
		}
		/// <summary>
		///  Big files can be broken down into chunks during snapshotting if needed.
		///  The chunk size can be specified in bytes or by using size value notation,
		///  i.e. 1g, 10m, 5k. Defaults to 64m (64m max)
		/// </summary>
		/// <param name="chunkSize"></param>
		public AzureRepositoryDescriptor ChunkSize(string chunkSize)
		{
			Self.Settings["chunk_size"] = chunkSize;
			return this;
		}

	}
}
