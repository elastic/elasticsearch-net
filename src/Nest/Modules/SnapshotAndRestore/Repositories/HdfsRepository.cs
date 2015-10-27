using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(RepositoryJsonConverter))]
	public interface IHdfsRepository : IRepository
	{
		[JsonProperty("uri")]
		string Uri { get; set; }

		[JsonProperty("path")]
		string Path { get; set; }

		[JsonProperty("load_defaults")]
		bool LoadDefaults { get; set; }

		[JsonProperty("conf_location")]
		string ConfigurationLocation { get; set; }

		[JsonProperty("compress")]
		bool Compress { get; set; }

		[JsonProperty("concurrent_streams")]
		int ConcurrentStreams { get; set; }

		[JsonProperty("chunk_size")]
		string ChunkSize { get; set; }

		[JsonIgnore]
		Dictionary<string, object> InlineHadoopConfiguration { get; set; }
	}

	public class HdfsRepository : IHdfsRepository
	{
		public HdfsRepository(string path)
		{
			this.Path = path;
		}

		string IRepository.Type { get; } = "hdfs";
		public string ChunkSize { get; set; }
		public bool Compress { get; set; }
		public int ConcurrentStreams { get; set; }
		public string ConfigurationLocation { get; set; }
		public Dictionary<string, object> InlineHadoopConfiguration { get; set; }
		public bool LoadDefaults { get; set; }
		public string Path { get; set; }
		public string Uri { get; set; }
	}

	public class HdfsRepositoryDescriptor 
		: DescriptorBase<HdfsRepositoryDescriptor, IHdfsRepository>, IHdfsRepository
	{
		string IRepository.Type { get { return "hdfs"; } } 
		string IHdfsRepository.Uri { get; set; }
		string IHdfsRepository.Path { get; set; }
		bool IHdfsRepository.LoadDefaults { get; set; }
		string IHdfsRepository.ConfigurationLocation { get; set; }
		bool IHdfsRepository.Compress { get; set; }
		int IHdfsRepository.ConcurrentStreams { get; set; }
		string IHdfsRepository.ChunkSize { get; set; }
		public Dictionary<string, object> InlineHadoopConfiguration { get; set; }

		/// <summary>
		/// optional - Hadoop file-system URI
		/// </summary>
		public HdfsRepositoryDescriptor Uri(string uri) => Assign(a => a.Uri = uri);

		/// <summary>
		///required - path with the file-system where data is stored/loaded
		/// </summary>
		public HdfsRepositoryDescriptor Path(string path) => Assign(a => a.Path = path);
		/// <summary>
		/// whether to load the default Hadoop configuration (default) or not
		/// </summary>
		/// <param name="loadDefaults"></param>
		public HdfsRepositoryDescriptor LoadDefaults(bool loadDefaults = true) => Assign(a => a.LoadDefaults = loadDefaults);

		/// <summary>
		/// Hadoop configuration XML to be loaded (use commas for multi values)
		/// </summary>
		/// <param name="configurationLocation"></param>
		public HdfsRepositoryDescriptor ConfigurationLocation(string configurationLocation) =>
			Assign(a => a.ConfigurationLocation = configurationLocation);

		/// <summary>
		/// 'inlined' key=value added to the Hadoop configuration
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public HdfsRepositoryDescriptor InlinedHadoopConfiguration(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> inlineConfig) => Assign(a =>
			a.InlineHadoopConfiguration = inlineConfig(new FluentDictionary<string, object>())
		);

		/// <summary>
		/// When set to true metadata files are stored in compressed format. This setting doesn't 
		/// affect index files that are already compressed by default. Defaults to false.
		/// </summary>
		/// <param name="compress"></param>
		public HdfsRepositoryDescriptor Compress(bool compress = true) => Assign(a => a.Compress = compress);

		/// <summary>
		/// Throttles the number of streams (per node) preforming snapshot operation. Defaults to 5
		/// </summary>
		/// <param name="concurrentStreams"></param>
		public HdfsRepositoryDescriptor ConcurrentStreams(int concurrentStreams) => Assign(a => a.ConcurrentStreams = concurrentStreams);

		/// <summary>
		///  Big files can be broken down into chunks during snapshotting if needed.
		///  The chunk size can be specified in bytes or by using size value notation,
		///  i.e. 1g, 10m, 5k. Disabled by default
		/// </summary>
		/// <param name="chunkSize"></param>
		public HdfsRepositoryDescriptor ChunkSize(string chunkSize) => Assign(a => a.ChunkSize = chunkSize);
	}
}
