using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface IFileSystemRepository : IRepository<IFileSystemRepositorySettings> { }

	public class FileSystemRepository : IFileSystemRepository
	{
		public FileSystemRepository(FileSystemRepositorySettings settings)
		{
			Settings = settings;
		}

		public IFileSystemRepositorySettings Settings { get; set; }
		public string Type { get; } = "fs";
	}

	public interface IFileSystemRepositorySettings : IRepositorySettings
	{
		[JsonProperty("location")]
		string Location { get; set; }

		[JsonProperty("compress")]
		bool? Compress { get; set; }

		[JsonProperty("concurrent_streams")]
		int? ConcurrentStreams { get; set; }

		[JsonProperty("chunk_size")]
		string ChunkSize { get; set; }

		[JsonProperty("max_restore_bytes_per_second")]
		string RestoreBytesPerSecondMaximum { get; set; }

		[JsonProperty("max_snapshot_bytes_per_second")]
		string SnapshotBytesPerSecondMaximum { get; set; }
	}

	public class FileSystemRepositorySettings : IFileSystemRepositorySettings
	{
		internal FileSystemRepositorySettings() { }
		public FileSystemRepositorySettings(string location)
		{
			this.Location = location;
		}

		public string Location { get; set; }

		public bool? Compress { get; set; }

		public int? ConcurrentStreams { get; set; }

		public string ChunkSize { get; set; }

		public string RestoreBytesPerSecondMaximum { get; set; }

		public string SnapshotBytesPerSecondMaximum { get; set; }
	}

	public class FileSystemRepositorySettingsDescriptor 
		: DescriptorBase<FileSystemRepositorySettingsDescriptor, IFileSystemRepositorySettings>, IFileSystemRepositorySettings
	{
		string IFileSystemRepositorySettings.Location { get; set; }
		bool? IFileSystemRepositorySettings.Compress  { get; set; }
		int? IFileSystemRepositorySettings.ConcurrentStreams { get; set; }
		string IFileSystemRepositorySettings.ChunkSize { get; set; }
		string IFileSystemRepositorySettings.RestoreBytesPerSecondMaximum { get; set; }
		string IFileSystemRepositorySettings.SnapshotBytesPerSecondMaximum { get; set; }

		/// <summary>
		/// Location of the snapshots. Mandatory.
		/// </summary>
		/// <param name="location"></param>
		public FileSystemRepositorySettingsDescriptor Location(string location) => Assign(a => a.Location = location);

		/// <summary>
		/// Turns on compression of the snapshot files. Defaults to true.
		/// </summary>
		/// <param name="compress"></param>
		public FileSystemRepositorySettingsDescriptor Compress(bool compress = true) => Assign(a => a.Compress = compress);

		/// <summary>
		/// Throttles the number of streams (per node) preforming snapshot operation. Defaults to 5
		/// </summary>
		/// <param name="concurrentStreams"></param>
		public FileSystemRepositorySettingsDescriptor ConcurrentStreams(int concurrentStreams) => Assign(a => a.ConcurrentStreams = concurrentStreams);

		/// <summary>
		/// Big files can be broken down into chunks during snapshotting if needed. 
		/// The chunk size can be specified in bytes or by using size value notation, i.e. 1g, 10m, 5k. 
		/// Defaults to null (unlimited chunk size).
		/// </summary>
		/// <param name="chunkSize"></param>
		public FileSystemRepositorySettingsDescriptor ChunkSize(string chunkSize) => Assign(a => a.ChunkSize = chunkSize);

		/// <summary>
		/// Throttles per node restore rate. Defaults to 20mb per second.
		/// </summary>
		/// <param name="maximumBytesPerSecond"></param>
		public FileSystemRepositorySettingsDescriptor RestoreBytesPerSecondMaximum(string maximumBytesPerSecond) =>
			Assign(a => a.RestoreBytesPerSecondMaximum = maximumBytesPerSecond);

		/// <summary>
		/// Throttles per node snapshot rate. Defaults to 20mb per second. 
		/// </summary>
		/// <param name="maximumBytesPerSecond"></param>
		public FileSystemRepositorySettingsDescriptor SnapshotBytesPerSecondMaximum(string maximumBytesPerSecond) =>
			Assign(a => a.SnapshotBytesPerSecondMaximum = maximumBytesPerSecond);
	}

	public class FileSystemRepositoryDescriptor 
		: DescriptorBase<FileSystemRepositoryDescriptor, IFileSystemRepository>, IFileSystemRepository
	{
		IFileSystemRepositorySettings IRepository<IFileSystemRepositorySettings>.Settings { get; set; }
		string ISnapshotRepository.Type { get; } = "fs";

		public FileSystemRepositoryDescriptor Settings(string location, Func<FileSystemRepositorySettingsDescriptor, IFileSystemRepositorySettings> settingsSelector = null) =>
			Assign(a => a.Settings = settingsSelector.InvokeOrDefault(new FileSystemRepositorySettingsDescriptor().Location(location)));
	}
}
