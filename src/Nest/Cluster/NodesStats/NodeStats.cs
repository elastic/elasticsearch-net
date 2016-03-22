using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class NodeStats
	{
		[JsonProperty("timestamp")]
		public long Timestamp { get; internal set; }

		[JsonProperty("name")]
		public string Name { get; internal set; }
		
		[JsonProperty("transport_address")]
		public string TransportAddress { get; internal set; }

		[JsonProperty("host")]
		public string Host { get; internal set; } 

		[JsonProperty("ip")]
		public IEnumerable<string> Ip { get; internal set; } 

		[JsonProperty("indices")]
		public IndexStats Indices { get; internal set; }

		[JsonProperty("os")]
		public OperatingSystemStats OperatingSystem { get; internal set; }

		[JsonProperty("process")]
		public ProcessStats Process { get; internal set; }

		[JsonProperty("script")]
		public ScriptStats Script { get; internal set; }

		[JsonProperty("jvm")]
		public NodeJvmStats Jvm { get; internal set; }

		[JsonProperty("thread_pool")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		public Dictionary<string, ThreadCountStats> ThreadPool { get; internal set; }

		[JsonProperty("breakers")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		public Dictionary<string, BreakerStats> Breakers { get; internal set; }

		[JsonProperty("fs")]
		public FileSystemStats FileSystem { get; internal set; }

		[JsonProperty("transport")]
		public TransportStats Transport { get; internal set; }

		[JsonProperty("http")]
		public HttpStats Http { get; internal set; }
	}

	[JsonObject]
	public class ScriptStats
	{
		[JsonProperty("cache_evictions")]
		public long CacheEvictions { get; internal set; }
		[JsonProperty("compilations")]
		public long Compilations { get; internal set; }
	}

	[JsonObject]
	public class BreakerStats
	{
		[JsonProperty("estimated_size")]
		public string EstimatedSize { get; internal set; }
		[JsonProperty("estimated_size_in_bytes")]
		public long EstimatedSizeInBytes { get; internal set; }
		[JsonProperty("limit_size")]
		public string LimitSize { get; internal set; }
		[JsonProperty("limit_size_in_bytes")]
		public long LimitSizeInBytes { get; internal set; }
		[JsonProperty("overhead")]
		public float Overhead { get; internal set; }
		[JsonProperty("tripped")]
		public float Tripped { get; internal set; }
	}

	[JsonObject]
	public class OperatingSystemStats 
	{
		[JsonProperty("timestamp")]
		public long Timestamp { get; internal set; }
		[JsonProperty("load_average")]
		public float LoadAverage { get; internal set; }

		[JsonProperty("mem")]
		public ExtendedMemoryStats Memory { get; internal set; }
		[JsonProperty("swap")]
		public MemoryStats Swap { get; internal set; }

		[JsonObject]
		public class MemoryStats
		{
			[JsonProperty("total")]
			public string Total { get; internal set; }

			[JsonProperty("total_in_bytes")]
			public long TotalInBytes { get; internal set; }

			[JsonProperty("free")]
			public string Free { get; internal set; }

			[JsonProperty("free_in_bytes")]
			public long FreeInBytes { get; internal set; }

			[JsonProperty("used")]
			public string Used { get; internal set; }

			[JsonProperty("used_in_bytes")]
			public long UsedInBytes { get; internal set; }
		}

		[JsonObject]
		public class ExtendedMemoryStats : MemoryStats
		{
			[JsonProperty("free_percent")]
			public int FreePercent { get; internal set; }
			[JsonProperty("used_percent")]
			public int UsedPercent { get; internal set; }
		}
	}

	[JsonObject]
	public class ProcessStats
	{
		[JsonProperty("timestamp")]
		public long Timestamp { get; internal set; }
		[JsonProperty("open_file_descriptors")]
		public int OpenFileDescriptors { get; internal set; }

		[JsonProperty("cpu")]
		public CPUStats CPU { get; internal set; }
		[JsonProperty("mem")]
		public MemoryStats Memory { get; internal set; }

		[JsonObject]
		public class CPUStats
		{
			[JsonProperty("percent")]
			public int Percent { get; internal set; }
			[JsonProperty("sys")]
			public string System { get; internal set; }
			[JsonProperty("sys_in_millis")]
			public long SystemInMilliseconds { get; internal set; }
			[JsonProperty("user")]
			public string User { get; internal set; }
			[JsonProperty("user_in_millis")]
			public long UserInMilliseconds { get; internal set; }
			[JsonProperty("total")]
			public string Total { get; internal set; }
			[JsonProperty("total_in_millis")]
			public long TotalInMilliseconds { get; internal set; }
		}

		[JsonObject]
		public class MemoryStats
		{
			[JsonProperty("resident")]
			public string Resident { get; internal set; }
			[JsonProperty("resident_in_bytes")]
			public long ResidentInBytes { get; internal set; }
			[JsonProperty("share")]
			public string Share { get; internal set; }
			[JsonProperty("share_in_bytes")]
			public long ShareInBytes { get; internal set; }
			[JsonProperty("total_virtual")]
			public string TotalVirtual { get; internal set; }
			[JsonProperty("total_virtual_in_bytes")]
			public long TotalVirtualInBytes { get; internal set; }
		}
	}

	[JsonObject]
	public class NodeJvmStats 
	{
		[JsonProperty("timestamp")]
		public long Timestamp { get; internal set; }
		[JsonProperty("uptime")]
		public string Uptime { get; internal set; }
		[JsonProperty("uptime_in_millis")]
		public long UptimeInMilliseconds { get; internal set; }
		[JsonProperty("mem")]
		public MemoryStats Memory { get; internal set; }
		[JsonProperty("threads")]
		public ThreadStats Threads { get; internal set; }
		[JsonProperty("gc")]
		public GarbageCollectionStats GarbageCollection { get; internal set; }

		[JsonProperty("buffer_pools")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		public Dictionary<string, NodeBufferPool> BufferPools { get; internal set; }
	
		[JsonProperty("classes")]
		public JvmClassesStats Classes { get; internal set; }

		[JsonObject]
		public class JvmClassesStats
		{
			[JsonProperty("current_loaded_count")]
			public long CurrentLoadedCount { get; internal set; }
			[JsonProperty("total_loaded_count")]
			public long TotalLoadedCount { get; internal set; }
			[JsonProperty("total_unloaded_count")]
			public long TotalUnloadedCount { get; internal set; }
		}

		[JsonObject]
		public class MemoryStats
		{
			[JsonProperty("heap_used")]
			public string HeapUsed { get; internal set; }
			[JsonProperty("heap_used_in_bytes")]
			public long HeapUsedInBytes { get; internal set; }
			[JsonProperty("heap_used_percent")]
			public long HeapUsedPercent { get; internal set; }
			[JsonProperty("heap_committed")]
			public string HeapCommitted { get; internal set; }
			[JsonProperty("heap_committed_in_bytes")]
			public long HeapCommittedInBytes { get; internal set; }
			[JsonProperty("heap_max")]
			public string HeapMax { get; internal set; }
			[JsonProperty("heap_max_in_bytes")]
			public long HeapMaxInBytes { get; internal set; }
			[JsonProperty("non_heap_used")]
			public string NonHeapUsed { get; internal set; }
			[JsonProperty("non_heap_used_in_bytes")]
			public long NonHeapUsedInBytes { get; internal set; }
			[JsonProperty("non_heap_committed")]
			public string NonHeapCommitted { get; internal set; }
			[JsonProperty("non_heap_committed_in_bytes")]
			public long NonHeapCommittedInBytes { get; internal set; }
			[JsonProperty("pools")]
			[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
			public Dictionary<string, JVMPool> Pools { get; internal set; }

			[JsonObject]
			public class JVMPool
			{
				[JsonProperty("used")]
				public string Used { get; internal set; }
				[JsonProperty("used_in_bytes")]
				public long UsedInBytes { get; internal set; }
				[JsonProperty("max")]
				public string Max { get; internal set; }
				[JsonProperty("max_in_bytes")]
				public long MaxInBytes { get; internal set; }
				[JsonProperty("peak_used")]
				public string PeakUsed { get; internal set; }
				[JsonProperty("peak_used_in_bytes")]
				public long PeakUsedInBytes { get; internal set; }
				[JsonProperty("peak_max")]
				public string PeakMax { get; internal set; }
				[JsonProperty("peak_max_in_bytes")]
				public long PeakMaxInBytes { get; internal set; }
			}
		}

		[JsonObject]
		public class ThreadStats
		{
			[JsonProperty("count")]
			public long Count { get; internal set; }
			[JsonProperty("peak_count")]
			public long PeakCount { get; internal set; }
		}

		[JsonObject]
		public class GarbageCollectionStats 
		{
			[JsonProperty("collectors")]
			[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
			public Dictionary<string, GarbageCollectionGenerationStats> Collectors { get; internal set; }
		}

		[JsonObject]
		public class GarbageCollectionGenerationStats
		{
			[JsonProperty("collection_count")]
			public long CollectionCount { get; internal set; }
			[JsonProperty("collection_time")]
			public string CollectionTime { get; internal set; }
			[JsonProperty("collection_time_in_millis")]
			public long CollectionTimeInMilliseconds { get; internal set; }
		}

		[JsonObject]
		public class NodeBufferPool
		{
			[JsonProperty("count")]
			public long Count { get; internal set; }
			[JsonProperty("used")]
			public string Used { get; internal set; }
			[JsonProperty("used_in_bytes")]
			public long UsedInBytes { get; internal set; }
			[JsonProperty("total_capacity")]
			public string TotalCapacity { get; internal set; }
			[JsonProperty("total_capacity_in_bytes")]
			public long TotalCapacityInBytes { get; internal set; }
		}
	}

	[JsonObject]
	public class ThreadCountStats
	{
		[JsonProperty("threads")]
		public long Threads { get; internal set; }
		[JsonProperty("queue")]
		public long Queue { get; internal set; }
		[JsonProperty("active")]
		public long Active { get; internal set; }
		[JsonProperty("rejected")]
		public long Rejected { get; internal set; }
		[JsonProperty("largest")]
		public long Largest { get; internal set; }
		[JsonProperty("completed")]
		public long Completed { get; internal set; }
	}

	[JsonObject]
	public class FileSystemStats
	{
		[JsonProperty("timestamp")]
		public long Timestamp { get; internal set; }
		[JsonProperty("total")]
		public TotalFileSystemStats Total { get; internal set; }
		[JsonProperty("data")]
		public IEnumerable<DataPathStats> Data { get; internal set; }

		public class TotalFileSystemStats
		{
			[JsonProperty("available")]
			public string Available { get; internal set; }
			[JsonProperty("available_in_bytes")]
			public long AvailableInBytes { get; internal set; }

			[JsonProperty("free")]
			public string Free { get; internal set; }
			[JsonProperty("free_in_bytes")]
			public long FreeInBytes { get; internal set; }

			[JsonProperty("total")]
			public string Total { get; internal set; }
			[JsonProperty("total_in_bytes")]
			public long TotalInBytes { get; internal set; }
		}

		[JsonObject]
		public class DataPathStats
		{
			[JsonProperty("path")]
			public string Path { get; internal set; }
			[JsonProperty("mount")]
			public string Mount { get; internal set; }
			[JsonProperty("type")]
			public string Type { get; internal set; }
			[JsonProperty("spins")]
			public bool? Spins { get; internal set; }
			[JsonProperty("total")]
			public string Total { get; internal set; }
			[JsonProperty("total_in_bytes")]
			public long TotalInBytes { get; internal set; }
			[JsonProperty("free")]
			public string Free { get; internal set; }
			[JsonProperty("free_in_bytes")]
			public long FreeInBytes { get; internal set; }
			[JsonProperty("available")]
			public string Available { get; internal set; }
			[JsonProperty("available_in_bytes")]
			public long AvailableInBytes { get; internal set; }
			[JsonProperty("disk_reads")]
			public long DiskReads { get; internal set; }
			[JsonProperty("disk_writes")]
			public long DiskWrites { get; internal set; }
			[JsonProperty("disk_read_size")]
			public string DiskReadSize { get; internal set; }
			[JsonProperty("disk_read_size_in_bytes")]
			public long DiskReadSizeInBytes { get; internal set; }
			[JsonProperty("disk_write_size")]
			public string DiskWriteSize { get; internal set; }
			[JsonProperty("disk_write_size_in_bytes")]
			public long DiskWriteSizeInBytes { get; internal set; }
			[JsonProperty("disk_queue")]
			public string DiskQueue { get; internal set; }
		}
	}

	[JsonObject]
	public class TransportStats
	{
		[JsonProperty("server_open")]
		public int ServerOpen { get; internal set; }
		[JsonProperty("rx_count")]
		public long RXCount { get; internal set; }
		[JsonProperty("rx_size")]
		public string RXSize { get; internal set; }
		[JsonProperty("rx_size_in_bytes")]
		public long RXSizeInBytes { get; internal set; }
		[JsonProperty("tx_count")]
		public long TXCount { get; internal set; }
		[JsonProperty("tx_size")]
		public string TXSize { get; internal set; }
		[JsonProperty("tx_size_in_bytes")]
		public long TXSizeInBytes { get; internal set; }
	}

	[JsonObject]
	public class HttpStats
	{
		[JsonProperty("current_open")]
		public int CurrentOpen { get; internal set; }
		[JsonProperty("total_opened")]
		public long TotalOpened { get; internal set; }
	}
}