using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(RepositoryJsonConverter))]
	public interface IAzureRepository : IRepository
	{
		[JsonProperty("container")]
		string Container { get; set; }

		[JsonProperty("base_path")]
		string BasePath { get; set; }

		[JsonProperty("compress")]
		bool Compress { get; set; }

		[JsonProperty("chunk_size")]
		string ChunkSize { get; set; }
	}

	public class AzureRepository : IAzureRepository
	{
		string IRepository.Type { get; } = "azure";
		public string Container { get; set; }
		public string BasePath { get; set; }
		public bool Compress { get; set; }
		public string ChunkSize { get; set; }
	}

	public class AzureRepositoryDescriptor
		: DescriptorBase<AzureRepositoryDescriptor, IAzureRepository>, IAzureRepository
	{
		string IRepository.Type { get { return "azure"; } }
		string IAzureRepository.Container { get; set; }
		string IAzureRepository.BasePath { get; set; }
		bool IAzureRepository.Compress { get; set; }
		string IAzureRepository.ChunkSize { get; set; }

		/// <summary>
		/// Container name. Defaults to elasticsearch-snapshots
		/// </summary>
		/// <param name="container"></param>
		public AzureRepositoryDescriptor Container(string container) => Assign(a => a.Container = container);

		/// <summary>
		///Specifies the path within container to repository data. Defaults to empty (root directory).
		/// </summary>
		/// <param name="basePath"></param>
		/// <returns></returns>
		public AzureRepositoryDescriptor BasePath(string basePath) => Assign(a => a.BasePath = basePath);

		/// <summary>
		/// When set to true metadata files are stored in compressed format. This setting doesn't 
		/// affect index files that are already compressed by default. Defaults to false.
		/// </summary>
		/// <param name="compress"></param>
		public AzureRepositoryDescriptor Compress(bool compress = true) => Assign(a => a.Compress = compress);

		/// <summary>
		///  Big files can be broken down into chunks during snapshotting if needed.
		///  The chunk size can be specified in bytes or by using size value notation,
		///  i.e. 1g, 10m, 5k. Defaults to 64m (64m max)
		/// </summary>
		/// <param name="chunkSize"></param>
		public AzureRepositoryDescriptor ChunkSize(string chunkSize) => Assign(a => a.ChunkSize = chunkSize);
	}
}
