using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface IAzureRepository : IRepository<IAzureRepositorySettings> { }

	public class AzureRepository : IAzureRepository
	{
		public IAzureRepositorySettings Settings { get; set; }
		public string Type { get; } = "azure";
	}

	public interface IAzureRepositorySettings : IRepositorySettings
	{
		[JsonProperty("container")]
		string Container { get; set; }

		[JsonProperty("base_path")]
		string BasePath { get; set; }

		[JsonProperty("compress")]
		bool? Compress { get; set; }

		[JsonProperty("chunk_size")]
		string ChunkSize { get; set; }
	}

	public class AzureRepositorySettings : IAzureRepositorySettings
	{
		[JsonProperty("container")]
		public string Container { get; set; }

		[JsonProperty("base_path")]
		public string BasePath { get; set; }

		[JsonProperty("compress")]
		public bool? Compress { get; set; }

		[JsonProperty("chunk_size")]
		public string ChunkSize { get; set; }
	}

	public class AzureRepositorySettingsDescriptor 
		: DescriptorBase<AzureRepositorySettingsDescriptor, IAzureRepositorySettings>, IAzureRepositorySettings
	{
		string IAzureRepositorySettings.BasePath { get; set; }
		string IAzureRepositorySettings.ChunkSize { get; set; }
		bool? IAzureRepositorySettings.Compress { get; set; }
		string IAzureRepositorySettings.Container { get; set; }

		/// <summary>
		/// Container name. Defaults to elasticsearch-snapshots
		/// </summary>
		/// <param name="container"></param>
		public AzureRepositorySettingsDescriptor Container(string container) => Assign(a => a.Container = container);

		/// <summary>
		///Specifies the path within container to repository data. Defaults to empty (root directory).
		/// </summary>
		/// <param name="basePath"></param>
		/// <returns></returns>
		public AzureRepositorySettingsDescriptor BasePath(string basePath) => Assign(a => a.BasePath = basePath);

		/// <summary>
		/// When set to true metadata files are stored in compressed format. This setting doesn't 
		/// affect index files that are already compressed by default. Defaults to false.
		/// </summary>
		/// <param name="compress"></param>
		public AzureRepositorySettingsDescriptor Compress(bool compress = true) => Assign(a => a.Compress = compress);

		/// <summary>
		///  Big files can be broken down into chunks during snapshotting if needed.
		///  The chunk size can be specified in bytes or by using size value notation,
		///  i.e. 1g, 10m, 5k. Defaults to 64m (64m max)
		/// </summary>
		/// <param name="chunkSize"></param>
		public AzureRepositorySettingsDescriptor ChunkSize(string chunkSize) => Assign(a => a.ChunkSize = chunkSize);
	}

	public class AzureRepositoryDescriptor
		: DescriptorBase<AzureRepositoryDescriptor, IAzureRepository>, IAzureRepository
	{
		IAzureRepositorySettings IRepository<IAzureRepositorySettings>.Settings { get; set; }
		string ISnapshotRepository.Type { get; } = "azure";

		public AzureRepositoryDescriptor Settings(Func<AzureRepositorySettingsDescriptor, IAzureRepositorySettings> settingsSelector) =>
			Assign(a => a.Settings = settingsSelector?.Invoke(new AzureRepositorySettingsDescriptor()));
	}
}
