using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class FileSystemRepository : IRepository
	{
		string IRepository.Type { get { return "fs"; } }
		public IDictionary<string, object> Settings { get; set; }
	}

	public class FileSystemRepositoryDescriptor : IRepository
	{
		string IRepository.Type { get { return "fs"; } }
		IDictionary<string, object> IRepository.Settings { get; set; }

		private IRepository Self { get { return this; } }

		public FileSystemRepositoryDescriptor()
		{
			Self.Settings = new Dictionary<string, object>();
		}
		/// <summary>
		/// Location of the snapshots. Mandatory.
		/// </summary>
		/// <param name="location"></param>
		public FileSystemRepositoryDescriptor Location(string location)
		{
			Self.Settings["location"] = location;
			return this;
		}
		/// <summary>
		/// Turns on compression of the snapshot files. Defaults to true.
		/// </summary>
		/// <param name="compress"></param>
		public FileSystemRepositoryDescriptor Compress(bool compress = true)
		{
			Self.Settings["compress"] = compress;
			return this;
		}
		/// <summary>
		/// Throttles the number of streams (per node) preforming snapshot operation. Defaults to 5
		/// </summary>
		/// <param name="concurrentStreams"></param>
		public FileSystemRepositoryDescriptor ConcurrentStreams(int concurrentStreams)
		{
			Self.Settings["concurrent_streams"] = concurrentStreams;
			return this;
		}
		/// <summary>
		/// Big files can be broken down into chunks during snapshotting if needed. 
		/// The chunk size can be specified in bytes or by using size value notation, i.e. 1g, 10m, 5k. 
		/// Defaults to null (unlimited chunk size).
		/// </summary>
		/// <param name="chunkSize"></param>
		public FileSystemRepositoryDescriptor ChunkSize(string chunkSize)
		{
			Self.Settings["chunk_size"] = chunkSize;
			return this;
		}
		/// <summary>
		/// Throttles per node restore rate. Defaults to 20mb per second.
		/// </summary>
		/// <param name="maximumBytesPerSecond"></param>
		public FileSystemRepositoryDescriptor RestoreBytesPerSecondMaximum(string maximumBytesPerSecond)
		{
			Self.Settings["max_restore_bytes_per_sec"] = maximumBytesPerSecond;
			return this;
		}
		/// <summary>
		/// Throttles per node snapshot rate. Defaults to 20mb per second. 
		/// </summary>
		/// <param name="maximumBytesPerSecond"></param>
		public FileSystemRepositoryDescriptor SnapshortBytesPerSecondMaximum(string maximumBytesPerSecond)
		{
			Self.Settings["max_snapshot_bytes_per_sec"] = maximumBytesPerSecond;
			return this;
		}


	}
}
