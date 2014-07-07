using System.Collections.Generic;

namespace Nest
{
	public class AzureRepositoryDescriptor : IRepository
	{
		public string Type { get { return "azure"; } }
		public IDictionary<string, object> Settings { get; private set; }

		public AzureRepositoryDescriptor()
		{
			this.Settings = new Dictionary<string, object>();
		}
		/// <summary>
		/// Container name. Defaults to elasticsearch-snapshots
		/// </summary>
		/// <param name="bucket"></param>
		public AzureRepositoryDescriptor Container(string bucket)
		{
			this.Settings["bucket"] = bucket;
			return this;
		}
		/// <summary>
		///Specifies the path within container to repository data. Defaults to empty (root directory).
		/// </summary>
		/// <param name="basePath"></param>
		/// <returns></returns>
		public AzureRepositoryDescriptor BasePath(string basePath)
		{
			this.Settings["base_path"] = basePath;
			return this;
		}
		/// <summary>
		/// When set to true metadata files are stored in compressed format. This setting doesn't 
		/// affect index files that are already compressed by default. Defaults to false.
		/// </summary>
		/// <param name="compress"></param>
		public AzureRepositoryDescriptor Compress(bool compress = true)
		{
			this.Settings["compress"] = compress;
			return this;
		}
		/// <summary>
		/// Throttles the number of streams (per node) preforming snapshot operation. Defaults to 5
		/// </summary>
		/// <param name="concurrentStreams"></param>
		public AzureRepositoryDescriptor ConcurrentStreams(int concurrentStreams)
		{
			this.Settings["concurrent_streams"] = concurrentStreams;
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
			this.Settings["chunk_size"] = chunkSize;
			return this;
		}

	}
}
