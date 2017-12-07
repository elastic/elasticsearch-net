using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class NodeInfo
	{
		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("transport_address")]
		public string TransportAddress { get; internal set; }

		[JsonProperty("host")]
		public string Host { get; internal set; }

		[JsonProperty("ip")]
		public string Ip { get; internal set; }

		[JsonProperty("version")]
		public string Version { get; internal set; }

		[JsonProperty("build_hash")]
		public string BuildHash { get; internal set; }

		/// <summary>
		/// All of the different roles that the node fulfills. An empty
		/// collection means that the node is a coordinating only node.
		/// </summary>
		[JsonProperty("roles")]
		public List<NodeRole> Roles { get; internal set; }

		//TODO why is this using DynamicBody
		[JsonProperty("settings")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, object>))]
		public DynamicBody Settings { get; internal set; }

		[JsonProperty("os")]
		public NodeOperatingSystemInfo OperatingSystem { get; internal set; }

		[JsonProperty("process")]
		public NodeProcessInfo Process { get; internal set; }

		[JsonProperty("jvm")]
		public NodeJvmInfo Jvm { get; internal set; }

		[JsonProperty("thread_pool")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, NodeThreadPoolInfo>))]
		public Dictionary<string, NodeThreadPoolInfo> ThreadPool { get; internal set; }

		[JsonProperty("network")]
		public NodeInfoNetwork Network { get; internal set; }

		[JsonProperty("transport")]
		public NodeInfoTransport Transport { get; internal set; }

		[JsonProperty("http")]
		public NodeInfoHttp Http { get; internal set; }

		[JsonProperty("plugins")]
		public List<PluginStats> Plugins { get; internal set; }
	}

	[JsonObject]
	public class NodeOperatingSystemInfo
	{
		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("arch")]
		public string Architecture { get; internal set; }

		[JsonProperty("version")]
		public string Version { get; internal set; }

		[JsonProperty("refresh_interval_in_millis")]
		public int RefreshInterval { get; internal set; }

		[JsonProperty("available_processors")]
		public int AvailableProcessors { get; internal set; }

		[JsonProperty("cpu")]
		public NodeInfoOSCPU Cpu { get; internal set; }

		[JsonProperty("mem")]
		public NodeInfoMemory Mem { get; internal set; }

		[JsonProperty("swap")]
		public NodeInfoMemory Swap { get; internal set; }
	}

	[JsonObject]
	public class NodeInfoOSCPU
	{
		[JsonProperty("vendor")]
		public string Vendor { get; internal set; }
		[JsonProperty("model")]
		public string Model { get; internal set; }
		[JsonProperty("mhz")]
		public int Mhz { get; internal set; }
		[JsonProperty("total_cores")]
		public int TotalCores { get; internal set; }
		[JsonProperty("total_sockets")]
		public int TotalSockets { get; internal set; }
		[JsonProperty("cores_per_socket")]
		public int CoresPerSocket { get; internal set; }
		[JsonProperty("cache_size")]
		public string CacheSize { get; internal set; }
		[JsonProperty("cache_size_in_bytes")]
		public int CacheSizeInBytes { get; internal set; }
	}

	[JsonObject]
	public class NodeInfoMemory
	{
		[JsonProperty("total")]
		public string Total { get; internal set; }
		[JsonProperty("total_in_bytes")]
		public long TotalInBytes { get; internal set; }
	}

	[JsonObject]
	public class NodeProcessInfo
	{
		[JsonProperty("refresh_interval_in_millis")]
		public long RefreshIntervalInMilliseconds { get; internal set; }

		[JsonProperty("id")]
		public long Id { get; internal set; }

		[JsonProperty("mlockall")]
		public bool MlockAll { get; internal set; }
	}

	[JsonObject]
	public class NodeJvmInfo
	{
		[JsonProperty("pid")]
		public int PID { get; internal set; }
		[JsonProperty("version")]
		public string Version { get; internal set; }
		[JsonProperty("vm_name")]
		public string VMName { get; internal set; }
		[JsonProperty("vm_version")]
		public string VMVersion { get; internal set; }
		[JsonProperty("vm_vendor")]
		public string VMVendor { get; internal set; }
		[JsonProperty("memory_pools")]
		public IEnumerable<string> MemoryPools { get; internal set; }
		[JsonProperty("gc_collectors")]
		public IEnumerable<string> GCCollectors { get; internal set; }
		[JsonProperty("start_time_in_millis")]
		public long StartTime { get; internal set; }
		[JsonProperty("mem")]
		public NodeInfoJVMMemory Memory { get; internal set; }
	}

	[JsonObject]
	public class NodeInfoJVMMemory
	{
		[JsonProperty("heap_init")]
		public string HeapInit { get; internal set; }
		[JsonProperty("heap_init_in_bytes")]
		public long HeapInitInBytes { get; internal set; }
		[JsonProperty("heap_max")]
		public string HeapMax { get; internal set; }
		[JsonProperty("heap_max_in_bytes")]
		public long HeapMaxInBytes { get; internal set; }
		[JsonProperty("non_heap_init")]
		public string NonHeapInit { get; internal set; }
		[JsonProperty("non_heap_init_in_bytes")]
		public long NonHeapInitInBytes { get; internal set; }
		[JsonProperty("non_heap_max")]
		public string NonHeapMax { get; internal set; }
		[JsonProperty("non_heap_max_in_bytes")]
		public long NonHeapMaxInBytes { get; internal set; }
		[JsonProperty("direct_max")]
		public string DirectMax { get; internal set; }
		[JsonProperty("direct_max_in_bytes")]
		public long DirectMaxInBytes { get; internal set; }
	}

	[JsonObject]
	public class NodeThreadPoolInfo
	{
		[JsonProperty("type")]
		public string Type { get; internal set; }
		[JsonProperty("min")]
		public int? Min { get; internal set; }
		[JsonProperty("max")]
		public int? Max { get; internal set; }
		[JsonProperty("queue_size")]
		public int? QueueSize { get; internal set; }
		[JsonProperty("keep_alive")]
		public string KeepAlive { get; internal set; }
	}

	[JsonObject]
	public class NodeInfoNetwork
	{
		[JsonProperty("refresh_interval")]
		public int RefreshInterval { get; internal set; }
		[JsonProperty("primary_interface")]
		public NodeInfoNetworkInterface PrimaryInterface { get; internal set; }
	}

	[JsonObject]
	public class NodeInfoNetworkInterface
	{
		[JsonProperty("address")]
		public string Address { get; internal set; }
		[JsonProperty("name")]
		public string Name { get; internal set; }
		[JsonProperty("mac_address")]
		public string MacAddress { get; internal set; }
	}

	[JsonObject]
	public class NodeInfoTransport
	{
		[JsonProperty("bound_address")]
		public IEnumerable<string> BoundAddress { get; internal set; }
		[JsonProperty("publish_address")]
		public string PublishAddress { get; internal set; }
	}

	[JsonObject]
	public class NodeInfoHttp
	{
		[JsonProperty("bound_address")]
		public IEnumerable<string> BoundAddress { get; internal set; }
		[JsonProperty("publish_address")]
		public string PublishAddress { get; internal set; }
		[JsonProperty("max_content_length")]
		public string MaxContentLength { get; internal set; }
		[JsonProperty("max_content_length_in_bytes")]
		public long MaxContentLengthInBytes { get; internal set; }
	}
}
