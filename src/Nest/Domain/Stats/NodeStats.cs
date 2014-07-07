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
		[JsonProperty("hostname")]
		public string Hostname { get; internal set; }

		[JsonProperty("indices")]
		public NodeStatsIndexes Indices { get; internal set; }

		[JsonProperty("os")]
		public OSStats OS { get; internal set; }

		[JsonProperty("process")]
		public ProcessStats Process { get; internal set; }

		[JsonProperty("jvm")]
		public JVM JVM { get; internal set; }

		[JsonProperty("thread_pool")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public Dictionary<string, ThreadCountStats> ThreadPool { get; internal set; }


		[JsonProperty("network")]
		public NetworkStats Network { get; internal set; }

		[JsonProperty("fs")]
		public FileSystemStats FileSystem { get; internal set; }

		[JsonProperty("transport")]
		public TransportStats Transport { get; internal set; }

		[JsonProperty("http")]
		public HttpStats HTTP { get; internal set; }
	}

	[JsonObject]
	public class NodeStatsIndexes
	{
		[JsonProperty("store")]
		public IndexStoreStats Store { get; internal set; }
		[JsonProperty("docs")]
		public DocStats Docs { get; internal set; }
		[JsonProperty("indexing")]
		public IndexingStats Indexing { get; internal set; }
		[JsonProperty("get")]
		public GetStats Get { get; internal set; }
		[JsonProperty("search")]
		public SearchStats Search { get; internal set; }
		[JsonProperty("cache")]
		public IndexCacheStats Cache { get; internal set; }
		[JsonProperty("merges")]
		public MergesStats Merges { get; internal set; }
		[JsonProperty("refresh")]
		public RefreshStats Refresh { get; internal set; }
		[JsonProperty("flush")]
		public FlushStats Flush { get; internal set; }

		[JsonObject]
		public class IndexStoreStats : StoreStats
		{
			[JsonProperty("throttle_time")]
			public string ThrottleTime { get; internal set; }
			[JsonProperty("throttle_time_in_millis")]
			public long ThrottleTimeInMilliseconds { get; internal set; }
		}

		[JsonObject]
		public class IndexCacheStats
		{
			[JsonProperty(PropertyName = "field_evictions")]
			public long FieldEvictions { get; internal set; }
			[JsonProperty(PropertyName = "field_size")]
			public string FieldSize { get; internal set; }
			[JsonProperty(PropertyName = "field_size_in_bytes")]
			public long FieldSizeInBytes { get; internal set; }
			[JsonProperty(PropertyName = "filter_count")]
			public long FilterCount { get; internal set; }
			[JsonProperty(PropertyName = "filter_evictions")]
			public long FilterEvictions { get; internal set; }
			[JsonProperty(PropertyName = "filter_size")]
			public string FilterSize { get; internal set; }
			[JsonProperty(PropertyName = "filter_size_in_bytes")]
			public long FilterSizeInBytes { get; internal set; }
			[JsonProperty(PropertyName = "bloom_size")]
			public string BloomSize { get; internal set; }
			[JsonProperty(PropertyName = "bloom_size_in_bytes")]
			public long BloomSizeInBytes { get; internal set; }
			[JsonProperty(PropertyName = "id_cache_size")]
			public string IDCacheSize { get; internal set; }
			[JsonProperty(PropertyName = "id_cache_size_in_bytes")]
			public long IDCacheSizeInBytes { get; internal set; }
		}
	}

	[JsonObject]
	public class UptimeStats
	{
		[JsonProperty("timestamp")]
		public long Timestamp { get; internal set; }
		[JsonProperty("uptime")]
		public string Uptime { get; internal set; }
		[JsonProperty("uptime_in_millis")]
		public long UptimeInMilliseconds { get; internal set; }
		[JsonProperty("load_average")]
		public float[] LoadAverage { get; internal set; }
	}

	[JsonObject]
	public class OSStats : UptimeStats
	{
		[JsonProperty("CPU")]
		public CPUStats CPU { get; internal set; }
		[JsonProperty("mem")]
		public MemoryStats Memory { get; internal set; }
		[JsonProperty("swap")]
		public BaseMemoryStats Swap { get; internal set; }


		[JsonObject]
		public class CPUStats
		{
			[JsonProperty("sys")]
			public int System { get; internal set; }
			[JsonProperty("user")]
			public int User { get; internal set; }
			[JsonProperty("idle")]
			public int Idle { get; internal set; }
		}

		[JsonObject]
		public class BaseMemoryStats
		{
			[JsonProperty("used")]
			public string Used { get; internal set; }
			[JsonProperty("used_in_bytes")]
			public long UsedInBytes { get; internal set; }
			[JsonProperty("free")]
			public string Free { get; internal set; }
			[JsonProperty("free_in_bytes")]
			public long FreeInBytes { get; internal set; }
		}

		[JsonObject]
		public class MemoryStats : BaseMemoryStats
		{
			[JsonProperty("free_percent")]
			public int FreePercent { get; internal set; }
			[JsonProperty("used_percent")]
			public int UsedPercent { get; internal set; }
			[JsonProperty("actual_free")]
			public string ActualFree { get; internal set; }
			[JsonProperty("actual_free_in_bytes")]
			public long ActualFreeInbytes { get; internal set; }
			[JsonProperty("actual_used")]
			public string ActualUsed { get; internal set; }
			[JsonProperty("actual_used_in_bytes")]
			public long ActualUsedInBytes { get; internal set; }
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
	public class JVM : UptimeStats
	{
		[JsonProperty("mem")]
		public MemoryStats Memory { get; internal set; }
		[JsonProperty("threads")]
		public ThreadStats Threads { get; internal set; }
		[JsonProperty("gc")]
		public GCOverallStats GC { get; internal set; }
		[JsonProperty("buffer_pools")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public Dictionary<string, NodeBufferPool> BufferPools { get; internal set; }

		[JsonObject]
		public class MemoryStats
		{
			[JsonProperty("heap_used")]
			public string HeapUsed { get; internal set; }
			[JsonProperty("heap_used_in_bytes")]
			public long HeapUsedInBytes { get; internal set; }
			[JsonProperty("heap_committed")]
			public string HeapCommitted { get; internal set; }
			[JsonProperty("heap_committed_in_bytes")]
			public long HeapCommittedInBytes { get; internal set; }
			[JsonProperty("non_heap_used")]
			public string NonHeapUsed { get; internal set; }
			[JsonProperty("non_heap_used_in_bytes")]
			public long NonHeapUsedInBytes { get; internal set; }
			[JsonProperty("non_heap_committed")]
			public string NonHeapCommitted { get; internal set; }
			[JsonProperty("non_heap_committed_in_bytes")]
			public long NonHeapCommittedInBytes { get; internal set; }
			[JsonProperty("pools")]
			[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
			public Dictionary<string, JVMPool> Pools { get; internal set; }

			[JsonObject]
			public class JVMPool
			{
				[JsonProperty("used")]
				public string Used { get; internal set; }
				[JsonProperty("")]
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
		public class GCOverallStats : GarbageCollectorStats
		{
			[JsonProperty("Collectors")]
			[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
			public Dictionary<string, GarbageCollectorStats> collectors { get; internal set; }
		}

		[JsonObject]
		public class GarbageCollectorStats
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
	public class NetworkStats
	{
		[JsonProperty("tcp")]
		public TCPStats tcp { get; internal set; }

		[JsonObject]
		public class TCPStats
		{
			[JsonProperty("active_opens")]
			public long ActiveOpens { get; internal set; }
			[JsonProperty("passive_opens")]
			public long PassiceOpens { get; internal set; }
			[JsonProperty("curr_estab")]
			public long CurrentEstablished { get; internal set; }
			[JsonProperty("in_segs")]
			public long InSegments { get; internal set; }
			[JsonProperty("out_segs")]
			public long OutSegments { get; internal set; }
			[JsonProperty("retrans_segs")]
			public long RetransmittedSegments { get; internal set; }
			[JsonProperty("estab_resets")]
			public long EstablishedResets { get; internal set; }
			[JsonProperty("attempt_fails")]
			public long AttemptFails { get; internal set; }
			[JsonProperty("in_errs")]
			public long InErrors { get; internal set; }
			[JsonProperty("out_rsts")]
			public long OutResets { get; internal set; }
		}
	}

	[JsonObject]
	public class FileSystemStats
	{
		[JsonProperty("timestamp")]
		public long Timestamp { get; internal set; }
		[JsonProperty("data")]
		public DatumStats[] Data { get; internal set; }

		[JsonObject]
		public class DatumStats
		{
			[JsonProperty("path")]
			public string Path { get; internal set; }
			[JsonProperty("mount")]
			public string Mount { get; internal set; }
			[JsonProperty("dev")]
			public string Dev { get; internal set; }
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