using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class NodeInfo
	{
		[JsonProperty(PropertyName = "name")]
		public string Name { get; internal set; }

		[JsonProperty(PropertyName = "transport_address")]
		public string TransportAddress { get; internal set; }

		[JsonProperty(PropertyName = "host")]
		public string Host { get; internal set; }

		[JsonProperty(PropertyName = "ip")]
		public string Ip { get; internal set; }

		[JsonProperty(PropertyName = "version")]
		public string Version { get; internal set; }

		[JsonProperty(PropertyName = "build_hash")]
		public string BuildHash { get; internal set; }

		[JsonProperty(PropertyName = "http_address")]
		public string HttpAddress { get; internal set; }

		[JsonProperty(PropertyName = "settings")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		public DynamicResponse Settings { get; internal set; }

		[JsonProperty(PropertyName = "os")]
		public NodeOperatingSystemInfo OperatingSystem { get; internal set; }

		[JsonProperty(PropertyName = "process")]
		public NodeProcessInfo Process { get; internal set; }

		[JsonProperty(PropertyName = "jvm")]
		public NodeJvmInfo Jvm { get; internal set; }

		[JsonProperty(PropertyName = "thread_pool")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		public Dictionary<string, NodeThreadPoolInfo> ThreadPool { get; internal set; }

		[JsonProperty(PropertyName = "network")]
		public NodeInfoNetwork Network { get; internal set; }

		[JsonProperty(PropertyName = "transport")]
		public NodeInfoTransport Transport { get; internal set; }

		[JsonProperty(PropertyName = "http")]
		public NodeInfoHttp Http { get; internal set; }

		[JsonProperty("plugins")]
		public List<PluginStats> Plugins { get; internal set; }

		[JsonProperty("roles")]
		public List<NodeRole> Roles { get; internal set; }
	}

	[JsonObject]
	public class NodeOperatingSystemInfo
	{
		[JsonProperty(PropertyName = "name")]
		public string Name { get; internal set; }

		[JsonProperty(PropertyName = "arch")]
		public string Architecture { get; internal set; }

		[JsonProperty(PropertyName = "version")]
		public string Version { get; internal set; }

		[JsonProperty(PropertyName = "refresh_interval_in_millis")]
		public int RefreshInterval { get; internal set; }

		[JsonProperty(PropertyName = "available_processors")]
		public int AvailableProcessors { get; internal set; }

		[JsonProperty(PropertyName = "cpu")]
		public NodeInfoOSCPU Cpu { get; internal set; }

		[JsonProperty(PropertyName = "mem")]
		public NodeInfoMemory Mem { get; internal set; }

		[JsonProperty(PropertyName = "swap")]
		public NodeInfoMemory Swap { get; internal set; }
	}

	[JsonObject]
	public class NodeInfoOSCPU
	{
		[JsonProperty(PropertyName = "vendor")]
		public string Vendor { get; internal set; }
		[JsonProperty(PropertyName = "model")]
		public string Model { get; internal set; }
		[JsonProperty(PropertyName = "mhz")]
		public int Mhz { get; internal set; }
		[JsonProperty(PropertyName = "total_cores")]
		public int TotalCores { get; internal set; }
		[JsonProperty(PropertyName = "total_sockets")]
		public int TotalSockets { get; internal set; }
		[JsonProperty(PropertyName = "cores_per_socket")]
		public int CoresPerSocket { get; internal set; }
		[JsonProperty(PropertyName = "cache_size")]
		public string CacheSize { get; internal set; }
		[JsonProperty(PropertyName = "cache_size_in_bytes")]
		public int CacheSizeInBytes { get; internal set; }
	}

	[JsonObject]
	public class NodeInfoMemory
	{
		[JsonProperty(PropertyName = "total")]
		public string Total { get; internal set; }
		[JsonProperty(PropertyName = "total_in_bytes")]
		public long TotalInBytes { get; internal set; }
	}

	[JsonObject]
	public class NodeProcessInfo
	{
		[JsonProperty(PropertyName = "refresh_interval_in_millis")]
		public long RefreshIntervalInMilliseconds { get; internal set; }

		[JsonProperty(PropertyName = "id")]
		public long Id { get; internal set; }

		[JsonProperty(PropertyName = "mlockall")]
		public bool MlockAll { get; internal set; }
	}

	[JsonObject]
	public class NodeJvmInfo
	{
		[JsonProperty(PropertyName = "pid")]
		public int PID { get; internal set; }
		[JsonProperty(PropertyName = "version")]
		public string Version { get; internal set; }
		[JsonProperty(PropertyName = "vm_name")]
		public string VMName { get; internal set; }
		[JsonProperty(PropertyName = "vm_version")]
		public string VMVersion { get; internal set; }
		[JsonProperty(PropertyName = "vm_vendor")]
		public string VMVendor { get; internal set; }
		[JsonProperty(PropertyName = "memory_pools")]
		public IEnumerable<string> MemoryPools { get; internal set; }
		[JsonProperty(PropertyName = "gc_collectors")]
		public IEnumerable<string> GCCollectors { get; internal set; }
		[JsonProperty(PropertyName = "start_time_in_millis")]
		public long StartTime { get; internal set; }
		[JsonProperty(PropertyName = "mem")]
		public NodeInfoJVMMemory Memory { get; internal set; }
	}

	[JsonObject]
	public class NodeInfoJVMMemory
	{
		[JsonProperty(PropertyName = "heap_init")]
		public string HeapInit { get; internal set; }
		[JsonProperty(PropertyName = "heap_init_in_bytes")]
		public long HeapInitInBytes { get; internal set; }
		[JsonProperty(PropertyName = "heap_max")]
		public string HeapMax { get; internal set; }
		[JsonProperty(PropertyName = "heap_max_in_bytes")]
		public long HeapMaxInBytes { get; internal set; }
		[JsonProperty(PropertyName = "non_heap_init")]
		public string NonHeapInit { get; internal set; }
		[JsonProperty(PropertyName = "non_heap_init_in_bytes")]
		public long NonHeapInitInBytes { get; internal set; }
		[JsonProperty(PropertyName = "non_heap_max")]
		public string NonHeapMax { get; internal set; }
		[JsonProperty(PropertyName = "non_heap_max_in_bytes")]
		public long NonHeapMaxInBytes { get; internal set; }
		[JsonProperty(PropertyName = "direct_max")]
		public string DirectMax { get; internal set; }
		[JsonProperty(PropertyName = "direct_max_in_bytes")]
		public long DirectMaxInBytes { get; internal set; }
	}

	[JsonObject]
	public class NodeThreadPoolInfo
	{
		[JsonProperty(PropertyName = "type")]
		public string Type { get; internal set; }
		[JsonProperty(PropertyName = "min")]
		public int? Min { get; internal set; }
		[JsonProperty(PropertyName = "max")]
		public int? Max { get; internal set; }
		[JsonProperty(PropertyName = "queue_size")]
		public int? QueueSize { get; internal set; }
		[JsonProperty(PropertyName = "keep_alive")]
		public string KeepAlive { get; internal set; }
	}

	[JsonObject]
	public class NodeInfoNetwork
	{
		[JsonProperty(PropertyName = "refresh_interval")]
		public int RefreshInterval { get; internal set; }
		[JsonProperty(PropertyName = "primary_interface")]
		public NodeInfoNetworkInterface PrimaryInterface { get; internal set; }
	}

	[JsonObject]
	public class NodeInfoNetworkInterface
	{
		[JsonProperty(PropertyName = "address")]
		public string Address { get; internal set; }
		[JsonProperty(PropertyName = "name")]
		public string Name { get; internal set; }
		[JsonProperty(PropertyName = "mac_address")]
		public string MacAddress { get; internal set; }
	}

	[JsonObject]
	public class NodeInfoTransport
	{
		[JsonProperty(PropertyName = "bound_address")]
		public IEnumerable<string> BoundAddress { get; internal set; }
		[JsonProperty(PropertyName = "publish_address")]
		public string PublishAddress { get; internal set; }
	}

	[JsonObject]
	public class NodeInfoHttp
	{
		[JsonProperty(PropertyName = "bound_address")]
		public IEnumerable<string> BoundAddress { get; internal set; }
		[JsonProperty(PropertyName = "publish_address")]
		public string PublishAddress { get; internal set; }
		[JsonProperty(PropertyName = "max_content_length")]
		public string MaxContentLength { get; internal set; }
		[JsonProperty(PropertyName = "max_content_length_in_bytes")]
		public long MaxContentLengthInBytes { get; internal set; }
	}
}
