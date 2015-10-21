using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class HdfsRepository : IRepository
	{
		string IRepository.Type { get { return "hdfs"; } }
		public IDictionary<string, object> Settings { get; set; }
	}

	public class HdfsRepositoryDescriptor : IRepository
	{
		string IRepository.Type { get { return "hdfs"; } }
		IDictionary<string, object> IRepository.Settings { get; set; }

		private IRepository Self => this;

		public HdfsRepositoryDescriptor()
		{
			Self.Settings = new Dictionary<string, object>();
		}
		/// <summary>
		/// optional - Hadoop file-system URI
		/// </summary>
		public HdfsRepositoryDescriptor Uri(string hdfsUri)
		{
			Self.Settings["uri"] = hdfsUri;
			return this;
		}
		/// <summary>
		///required - path with the file-system where data is stored/loaded
		/// </summary>
		public HdfsRepositoryDescriptor Path(string path)
		{
			Self.Settings["path"] = path;
			return this;
		}
		/// <summary>
		/// whether to load the default Hadoop configuration (default) or not
		/// </summary>
		/// <param name="loadDefaults"></param>
		public HdfsRepositoryDescriptor LoadDefaults(bool loadDefaults = true)
		{
			Self.Settings["load_defaults"] = loadDefaults;
			return this;
		}
		/// <summary>
		/// Hadoop configuration XML to be loaded (use commas for multi values)
		/// </summary>
		/// <param name="configurationLocation"></param>
		public HdfsRepositoryDescriptor ConfigurationLocation(string configurationLocation)
		{
			Self.Settings["conf_location"] = configurationLocation;
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
			Self.Settings[key] = value;
			return this;
		}
		/// <summary>
		/// When set to true metadata files are stored in compressed format. This setting doesn't 
		/// affect index files that are already compressed by default. Defaults to false.
		/// </summary>
		/// <param name="compress"></param>
		public HdfsRepositoryDescriptor Compress(bool compress = true)
		{
			Self.Settings["compress"] = compress;
			return this;
		}
		/// <summary>
		/// Throttles the number of streams (per node) preforming snapshot operation. Defaults to 5
		/// </summary>
		/// <param name="concurrentStreams"></param>
		public HdfsRepositoryDescriptor ConcurrentStreams(int concurrentStreams)
		{
			Self.Settings["concurrent_streams"] = concurrentStreams;
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
			Self.Settings["chunk_size"] = chunkSize;
			return this;
		}

	}
}
