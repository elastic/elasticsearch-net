// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[DataContract]
	public class NodeStats
	{
		[DataMember(Name = "adaptive_selection")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, AdaptiveSelectionStats>))]
		public IReadOnlyDictionary<string, AdaptiveSelectionStats> AdaptiveSelection { get; internal set; }
			= EmptyReadOnly<string, AdaptiveSelectionStats>.Dictionary;

		[DataMember(Name = "breakers")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, BreakerStats>))]
		public IReadOnlyDictionary<string, BreakerStats> Breakers { get; internal set; }

		[DataMember(Name = "fs")]
		public FileSystemStats FileSystem { get; internal set; }

		[DataMember(Name = "host")]
		public string Host { get; internal set; }

		[DataMember(Name = "http")]
		public HttpStats Http { get; internal set; }

		[DataMember(Name = "indices")]
		public IndexStats Indices { get; internal set; }

		[DataMember(Name = "ingest")]
		public NodeIngestStats Ingest { get; internal set; }

		[DataMember(Name = "ip")]
		[JsonFormatter(typeof(SingleOrEnumerableFormatter<string>))]
		public IEnumerable<string> Ip { get; internal set; }

		[DataMember(Name = "jvm")]
		public NodeJvmStats Jvm { get; internal set; }

		[DataMember(Name = "name")]
		public string Name { get; internal set; }

		[DataMember(Name = "os")]
		public OperatingSystemStats OperatingSystem { get; internal set; }

		[DataMember(Name = "process")]
		public ProcessStats Process { get; internal set; }

		/// <summary>
		/// All of the different roles that the node fulfills. An empty
		/// collection means that the node is a coordinating only node.
		/// </summary>
		[DataMember(Name = "roles")]
		public IEnumerable<NodeRole> Roles { get; internal set; }

		[DataMember(Name = "script")]
		public ScriptStats Script { get; internal set; }

		/// <summary>
		/// Available in Elasticsearch 7.8.0+
		/// </summary>
		[DataMember(Name = "script_cache")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, ScriptStats>))]
		public IReadOnlyDictionary<string, ScriptStats> ScriptCache { get; internal set; }

		[DataMember(Name = "thread_pool")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, ThreadCountStats>))]
		public IReadOnlyDictionary<string, ThreadCountStats> ThreadPool { get; internal set; }

		[DataMember(Name = "timestamp")]
		public long Timestamp { get; internal set; }

		[DataMember(Name = "transport")]
		public TransportStats Transport { get; internal set; }

		[DataMember(Name = "transport_address")]
		public string TransportAddress { get; internal set; }
	}


	[DataContract]
	public class ScriptStats
	{
		[DataMember(Name = "cache_evictions")]
		public long CacheEvictions { get; internal set; }

		[DataMember(Name = "compilations")]
		public long Compilations { get; internal set; }

		[DataMember(Name = "compilation_limit_triggered")]
		public long CompilationLimitTriggered { get; internal set; }
	}

	[DataContract]
	public class BreakerStats
	{
		[DataMember(Name = "estimated_size")]
		public string EstimatedSize { get; internal set; }

		[DataMember(Name = "estimated_size_in_bytes")]
		public long EstimatedSizeInBytes { get; internal set; }

		[DataMember(Name = "limit_size")]
		public string LimitSize { get; internal set; }

		[DataMember(Name = "limit_size_in_bytes")]
		public long LimitSizeInBytes { get; internal set; }

		[DataMember(Name = "overhead")]
		public float Overhead { get; internal set; }

		[DataMember(Name = "tripped")]
		public float Tripped { get; internal set; }
	}

	[DataContract]
	public class OperatingSystemStats
	{
		[DataMember(Name = "cpu")]
		public CPUStats Cpu { get; internal set; }

		[DataMember(Name = "mem")]
		public ExtendedMemoryStats Memory { get; internal set; }

		[DataMember(Name = "swap")]
		public MemoryStats Swap { get; internal set; }

		[DataMember(Name = "timestamp")]
		public long Timestamp { get; internal set; }

		[DataContract]
		public class CPUStats
		{
			[DataMember(Name = "load_average")]
			public LoadAverageStats LoadAverage { get; internal set; }

			[DataMember(Name = "percent")]
			public float Percent { get; internal set; }

			[DataContract]
			public class LoadAverageStats
			{
				[DataMember(Name = "15m")]
				public float? FifteenMinute { get; internal set; }

				[DataMember(Name = "5m")]
				public float? FiveMinute { get; internal set; }

				[DataMember(Name = "1m")]
				public float? OneMinute { get; internal set; }
			}
		}

		[DataContract]
		public class MemoryStats
		{
			[DataMember(Name = "free")]
			public string Free { get; internal set; }

			[DataMember(Name = "free_in_bytes")]
			public long FreeInBytes { get; internal set; }

			[DataMember(Name = "total")]
			public string Total { get; internal set; }

			[DataMember(Name = "total_in_bytes")]
			public long TotalInBytes { get; internal set; }

			[DataMember(Name = "used")]
			public string Used { get; internal set; }

			[DataMember(Name = "used_in_bytes")]
			public long UsedInBytes { get; internal set; }
		}

		[DataContract]
		public class ExtendedMemoryStats : MemoryStats
		{
			[DataMember(Name = "free_percent")]
			public int FreePercent { get; internal set; }

			[DataMember(Name = "used_percent")]
			public int UsedPercent { get; internal set; }
		}
	}

	[DataContract]
	public class ProcessStats
	{
		[DataMember(Name = "cpu")]
		public CPUStats CPU { get; internal set; }

		[DataMember(Name = "mem")]
		public MemoryStats Memory { get; internal set; }

		[DataMember(Name = "open_file_descriptors")]
		public int OpenFileDescriptors { get; internal set; }

		[DataMember(Name = "timestamp")]
		public long Timestamp { get; internal set; }

		[DataContract]
		public class CPUStats
		{
			[DataMember(Name = "percent")]
			public int Percent { get; internal set; }

			[DataMember(Name = "sys")]
			public string System { get; internal set; }

			[DataMember(Name = "sys_in_millis")]
			public long SystemInMilliseconds { get; internal set; }

			[DataMember(Name = "total")]
			public string Total { get; internal set; }

			[DataMember(Name = "total_in_millis")]
			public long TotalInMilliseconds { get; internal set; }

			[DataMember(Name = "user")]
			public string User { get; internal set; }

			[DataMember(Name = "user_in_millis")]
			public long UserInMilliseconds { get; internal set; }
		}

		[DataContract]
		public class MemoryStats
		{
			[DataMember(Name = "resident")]
			public string Resident { get; internal set; }

			[DataMember(Name = "resident_in_bytes")]
			public long ResidentInBytes { get; internal set; }

			[DataMember(Name = "share")]
			public string Share { get; internal set; }

			[DataMember(Name = "share_in_bytes")]
			public long ShareInBytes { get; internal set; }

			[DataMember(Name = "total_virtual")]
			public string TotalVirtual { get; internal set; }

			[DataMember(Name = "total_virtual_in_bytes")]
			public long TotalVirtualInBytes { get; internal set; }
		}
	}

	[DataContract]
	public class NodeJvmStats
	{
		[DataMember(Name = "buffer_pools")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, NodeBufferPool>))]
		public IReadOnlyDictionary<string, NodeBufferPool> BufferPools { get; internal set; }

		[DataMember(Name = "classes")]
		public JvmClassesStats Classes { get; internal set; }

		[DataMember(Name = "gc")]
		public GarbageCollectionStats GarbageCollection { get; internal set; }

		[DataMember(Name = "mem")]
		public MemoryStats Memory { get; internal set; }

		[DataMember(Name = "threads")]
		public ThreadStats Threads { get; internal set; }

		[DataMember(Name = "timestamp")]
		public long Timestamp { get; internal set; }

		[DataMember(Name = "uptime")]
		public string Uptime { get; internal set; }

		[DataMember(Name = "uptime_in_millis")]
		public long UptimeInMilliseconds { get; internal set; }

		[DataContract]
		public class JvmClassesStats
		{
			[DataMember(Name = "current_loaded_count")]
			public long CurrentLoadedCount { get; internal set; }

			[DataMember(Name = "total_loaded_count")]
			public long TotalLoadedCount { get; internal set; }

			[DataMember(Name = "total_unloaded_count")]
			public long TotalUnloadedCount { get; internal set; }
		}

		[DataContract]
		public class MemoryStats
		{
			[DataMember(Name = "heap_committed")]
			public string HeapCommitted { get; internal set; }

			[DataMember(Name = "heap_committed_in_bytes")]
			public long HeapCommittedInBytes { get; internal set; }

			[DataMember(Name = "heap_max")]
			public string HeapMax { get; internal set; }

			[DataMember(Name = "heap_max_in_bytes")]
			public long HeapMaxInBytes { get; internal set; }

			[DataMember(Name = "heap_used")]
			public string HeapUsed { get; internal set; }

			[DataMember(Name = "heap_used_in_bytes")]
			public long HeapUsedInBytes { get; internal set; }

			[DataMember(Name = "heap_used_percent")]
			public long HeapUsedPercent { get; internal set; }

			[DataMember(Name = "non_heap_committed")]
			public string NonHeapCommitted { get; internal set; }

			[DataMember(Name = "non_heap_committed_in_bytes")]
			public long NonHeapCommittedInBytes { get; internal set; }

			[DataMember(Name = "non_heap_used")]
			public string NonHeapUsed { get; internal set; }

			[DataMember(Name = "non_heap_used_in_bytes")]
			public long NonHeapUsedInBytes { get; internal set; }

			[DataMember(Name = "pools")]
			[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, JvmPool>))]
			public IReadOnlyDictionary<string, JvmPool> Pools { get; internal set; }

			[DataContract]
			public class JvmPool
			{
				[DataMember(Name = "max")]
				public string Max { get; internal set; }

				[DataMember(Name = "max_in_bytes")]
				public long MaxInBytes { get; internal set; }

				[DataMember(Name = "peak_max")]
				public string PeakMax { get; internal set; }

				[DataMember(Name = "peak_max_in_bytes")]
				public long PeakMaxInBytes { get; internal set; }

				[DataMember(Name = "peak_used")]
				public string PeakUsed { get; internal set; }

				[DataMember(Name = "peak_used_in_bytes")]
				public long PeakUsedInBytes { get; internal set; }

				[DataMember(Name = "used")]
				public string Used { get; internal set; }

				[DataMember(Name = "used_in_bytes")]
				public long UsedInBytes { get; internal set; }
			}
		}

		[DataContract]
		public class ThreadStats
		{
			[DataMember(Name = "count")]
			public long Count { get; internal set; }

			[DataMember(Name = "peak_count")]
			public long PeakCount { get; internal set; }
		}

		[DataContract]
		public class GarbageCollectionStats
		{
			[DataMember(Name = "collectors")]
			[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, GarbageCollectionGenerationStats>))]
			public IReadOnlyDictionary<string, GarbageCollectionGenerationStats> Collectors { get; internal set; }
		}

		[DataContract]
		public class GarbageCollectionGenerationStats
		{
			[DataMember(Name = "collection_count")]
			public long CollectionCount { get; internal set; }

			[DataMember(Name = "collection_time")]
			public string CollectionTime { get; internal set; }

			[DataMember(Name = "collection_time_in_millis")]
			public long CollectionTimeInMilliseconds { get; internal set; }
		}

		[DataContract]
		public class NodeBufferPool
		{
			[DataMember(Name = "count")]
			public long Count { get; internal set; }

			[DataMember(Name = "total_capacity")]
			public string TotalCapacity { get; internal set; }

			[DataMember(Name = "total_capacity_in_bytes")]
			public long TotalCapacityInBytes { get; internal set; }

			[DataMember(Name = "used")]
			public string Used { get; internal set; }

			[DataMember(Name = "used_in_bytes")]
			public long UsedInBytes { get; internal set; }
		}
	}

	[DataContract]
	public class ThreadCountStats
	{
		[DataMember(Name = "active")]
		public long Active { get; internal set; }

		[DataMember(Name = "completed")]
		public long Completed { get; internal set; }

		[DataMember(Name = "largest")]
		public long Largest { get; internal set; }

		[DataMember(Name = "queue")]
		public long Queue { get; internal set; }

		[DataMember(Name = "rejected")]
		public long Rejected { get; internal set; }

		[DataMember(Name = "threads")]
		public long Threads { get; internal set; }
	}

	[DataContract]
	public class FileSystemStats
	{
		[DataMember(Name = "data")]
		public IEnumerable<DataPathStats> Data { get; internal set; }

		[DataMember(Name = "timestamp")]
		public long Timestamp { get; internal set; }

		[DataMember(Name = "total")]
		public TotalFileSystemStats Total { get; internal set; }

		public class TotalFileSystemStats
		{
			[DataMember(Name = "available")]
			public string Available { get; internal set; }

			[DataMember(Name = "available_in_bytes")]
			public long AvailableInBytes { get; internal set; }

			[DataMember(Name = "free")]
			public string Free { get; internal set; }

			[DataMember(Name = "free_in_bytes")]
			public long FreeInBytes { get; internal set; }

			[DataMember(Name = "total")]
			public string Total { get; internal set; }

			[DataMember(Name = "total_in_bytes")]
			public long TotalInBytes { get; internal set; }
		}

		[DataContract]
		public class DataPathStats
		{
			[DataMember(Name = "available")]
			public string Available { get; internal set; }

			[DataMember(Name = "available_in_bytes")]
			public long AvailableInBytes { get; internal set; }

			[DataMember(Name = "disk_queue")]
			public string DiskQueue { get; internal set; }

			[DataMember(Name = "disk_reads")]
			public long DiskReads { get; internal set; }

			[DataMember(Name = "disk_read_size")]
			public string DiskReadSize { get; internal set; }

			[DataMember(Name = "disk_read_size_in_bytes")]
			public long DiskReadSizeInBytes { get; internal set; }

			[DataMember(Name = "disk_writes")]
			public long DiskWrites { get; internal set; }

			[DataMember(Name = "disk_write_size")]
			public string DiskWriteSize { get; internal set; }

			[DataMember(Name = "disk_write_size_in_bytes")]
			public long DiskWriteSizeInBytes { get; internal set; }

			[DataMember(Name = "free")]
			public string Free { get; internal set; }

			[DataMember(Name = "free_in_bytes")]
			public long FreeInBytes { get; internal set; }

			[DataMember(Name = "mount")]
			public string Mount { get; internal set; }

			[DataMember(Name = "path")]
			public string Path { get; internal set; }

			[DataMember(Name = "total")]
			public string Total { get; internal set; }

			[DataMember(Name = "total_in_bytes")]
			public long TotalInBytes { get; internal set; }

			[DataMember(Name = "type")]
			public string Type { get; internal set; }
		}
	}

	[DataContract]
	public class TransportStats
	{
		[DataMember(Name = "rx_count")]
		public long RxCount { get; internal set; }

		[DataMember(Name = "rx_size")]
		public string RxSize { get; internal set; }

		[DataMember(Name = "rx_size_in_bytes")]
		public long RxSizeInBytes { get; internal set; }

		[DataMember(Name = "server_open")]
		public int ServerOpen { get; internal set; }

		[DataMember(Name = "tx_count")]
		public long TxCount { get; internal set; }

		[DataMember(Name = "tx_size")]
		public string TxSize { get; internal set; }

		[DataMember(Name = "tx_size_in_bytes")]
		public long TxSizeInBytes { get; internal set; }
	}

	[DataContract]
	public class HttpStats
	{
		[DataMember(Name = "current_open")]
		public int CurrentOpen { get; internal set; }

		[DataMember(Name = "total_opened")]
		public long TotalOpened { get; internal set; }
	}
}
