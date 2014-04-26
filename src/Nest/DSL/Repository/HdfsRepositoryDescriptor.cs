using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class HdfsRepositoryDescriptor : IRepository
	{
		public string Type { get { return "hdfs"; } }
		public IDictionary<string, object> Settings { get; private set; }

		public HdfsRepositoryDescriptor()
		{
			this.Settings = new Dictionary<string, object>();
		}
		/// <summary>
		/// optional - Hadoop file-system URI
		/// </summary>
		/// <param name="bucket"></param>
		public HdfsRepositoryDescriptor Uri(string hdfsUri)
		{
			this.Settings["uri"] = hdfsUri;
			return this;
		}
		/// <summary>
		///required - path with the file-system where data is stored/loaded
		/// </summary>
		/// <param name="basePath"></param>
		/// <returns></returns>
		public HdfsRepositoryDescriptor Path(string path)
		{
			this.Settings["path"] = path;
			return this;
		}
		/// <summary>
		/// whether to load the default Hadoop configuration (default) or not
		/// </summary>
		/// <param name="loadDefaults"></param>
		public HdfsRepositoryDescriptor LoadDefaults(bool loadDefaults = true)
		{
			this.Settings["load_defaults"] = loadDefaults;
			return this;
		}
		/// <summary>
		/// Hadoop configuration XML to be loaded (use commas for multi values)
		/// </summary>
		/// <param name="configurationLocation"></param>
		public HdfsRepositoryDescriptor ConfigurationLocation(string configurationLocation)
		{
			this.Settings["conf_location"] = configurationLocation;
			return this;
		}
		/// <summary>
		/// 'inlined' key=value added to the Hadoop configuration
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public HdfsRepositoryDescriptor InlinedHadoopConfiguration(string key, object value)
		{
			key.ThrowIfNullOrEmpty("key");
			key = "conf." + key;
			this.Settings[key] = value;
			return this;
		}
		/// <summary>
		/// When set to true metadata files are stored in compressed format. This setting doesn't 
		/// affect index files that are already compressed by default. Defaults to false.
		/// </summary>
		/// <param name="compress"></param>
		public HdfsRepositoryDescriptor Compress(bool compress = true)
		{
			this.Settings["compress"] = compress;
			return this;
		}
		/// <summary>
		/// Throttles the number of streams (per node) preforming snapshot operation. Defaults to 5
		/// </summary>
		/// <param name="concurrentStreams"></param>
		public HdfsRepositoryDescriptor ConcurrentStreams(int concurrentStreams)
		{
			this.Settings["concurrent_streams"] = concurrentStreams;
			return this;
		}
		/// <summary>
		///  Big files can be broken down into chunks during snapshotting if needed.
		///  The chunk size can be specified in bytes or by using size value notation,
		///  i.e. 1g, 10m, 5k. Disabled by default
		/// </summary>
		/// <param name="chunkSize"></param>
		public HdfsRepositoryDescriptor ChunkSize(string chunkSize)
		{
			this.Settings["chunk_size"] = chunkSize;
			return this;
		}

	}
}
