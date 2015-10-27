using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(RepositoryJsonConverter))]
	public interface IFileSystemRepository : IRepository
	{
		[JsonProperty("location")]
		string Location { get; set; }

		[JsonProperty("compress")]
		bool Compress { get; set; }

		[JsonProperty("concurrent_streams")]
		int ConcurrentStreams { get; set; }

		[JsonProperty("chunk_size")]
		string ChunkSize { get; set; }

		[JsonProperty("max_restore_bytes_per_second")]
		string MaximumRestoreBytesPerSecond { get; set; }

		[JsonProperty("max_snapshot_bytes_per_second")]
		string MaximumSnapshotBytesPerSecond { get; set; }
	}

	public class FileSystemRepository : IFileSystemRepository
	{
		public FileSystemRepository(string location)
		{
			this.Location = location;
		}

		string IRepository.Type { get { return "fs"; } }

		public string Location { get; set; }

		public bool Compress { get; set; }

		public int ConcurrentStreams { get; set; }

		public string ChunkSize { get; set; }

		public string MaximumRestoreBytesPerSecond { get; set; }

		public string MaximumSnapshotBytesPerSecond { get; set; }
	}

	public class FileSystemRepositoryDescriptor 
		: DescriptorBase<FileSystemRepositoryDescriptor, IFileSystemRepository>, IFileSystemRepository
	{
		string IRepository.Type { get { return "fs"; } }
		string IFileSystemRepository.Location { get; set; }
		bool IFileSystemRepository.Compress  { get; set; }
		int IFileSystemRepository.ConcurrentStreams { get; set; }
		string IFileSystemRepository.ChunkSize { get; set; }
		string IFileSystemRepository.MaximumRestoreBytesPerSecond { get; set; }
		string IFileSystemRepository.MaximumSnapshotBytesPerSecond { get; set; }

		/// <summary>
		/// Location of the snapshots. Mandatory.
		/// </summary>
		/// <param name="location"></param>
		public FileSystemRepositoryDescriptor Location(string location) => Assign(a => a.Location = location);

		/// <summary>
		/// Turns on compression of the snapshot files. Defaults to true.
		/// </summary>
		/// <param name="compress"></param>
		public FileSystemRepositoryDescriptor Compress(bool compress = true) => Assign(a => a.Compress = compress);

		/// <summary>
		/// Throttles the number of streams (per node) preforming snapshot operation. Defaults to 5
		/// </summary>
		/// <param name="concurrentStreams"></param>
		public FileSystemRepositoryDescriptor ConcurrentStreams(int concurrentStreams) => Assign(a => a.ConcurrentStreams = concurrentStreams);

		/// <summary>
		/// Big files can be broken down into chunks during snapshotting if needed. 
		/// The chunk size can be specified in bytes or by using size value notation, i.e. 1g, 10m, 5k. 
		/// Defaults to null (unlimited chunk size).
		/// </summary>
		/// <param name="chunkSize"></param>
		public FileSystemRepositoryDescriptor ChunkSize(string chunkSize) => Assign(a => a.ChunkSize = chunkSize);

		/// <summary>
		/// Throttles per node restore rate. Defaults to 20mb per second.
		/// </summary>
		/// <param name="maximumBytesPerSecond"></param>
		public FileSystemRepositoryDescriptor RestoreBytesPerSecondMaximum(string maximumBytesPerSecond) =>
			Assign(a => a.MaximumRestoreBytesPerSecond = maximumBytesPerSecond);

		/// <summary>
		/// Throttles per node snapshot rate. Defaults to 20mb per second. 
		/// </summary>
		/// <param name="maximumBytesPerSecond"></param>
		public FileSystemRepositoryDescriptor SnapshotBytesPerSecondMaximum(string maximumBytesPerSecond) =>
			Assign(a => a.MaximumSnapshotBytesPerSecond = maximumBytesPerSecond);
	}
}
