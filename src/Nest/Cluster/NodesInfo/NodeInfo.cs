using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Utf8Json;

namespace Nest
{
	[DataContract]
	public class NodeInfo
	{
		[DataMember(Name = "build_hash")]
		public string BuildHash { get; internal set; }

		[DataMember(Name = "host")]
		public string Host { get; internal set; }

		[DataMember(Name = "http")]
		public NodeInfoHttp Http { get; internal set; }

		[DataMember(Name = "ip")]
		public string Ip { get; internal set; }

		[DataMember(Name = "jvm")]
		public NodeJvmInfo Jvm { get; internal set; }

		[DataMember(Name = "name")]
		public string Name { get; internal set; }

		[DataMember(Name = "network")]
		public NodeInfoNetwork Network { get; internal set; }

		[DataMember(Name = "os")]
		public NodeOperatingSystemInfo OperatingSystem { get; internal set; }

		[DataMember(Name = "plugins")]
		public List<PluginStats> Plugins { get; internal set; }

		[DataMember(Name = "process")]
		public NodeProcessInfo Process { get; internal set; }

		/// <summary>
		/// All of the different roles that the node fulfills. An empty
		/// collection means that the node is a coordinating only node.
		/// </summary>
		[DataMember(Name = "roles")]
		public List<NodeRole> Roles { get; internal set; }

		//TODO why is this using DynamicBody
		[DataMember(Name = "settings")]
		[JsonFormatter(typeof(VerbatimDictionaryInterfaceKeysFormatter<string, object>))]
		public DynamicBody Settings { get; internal set; }

		[DataMember(Name = "thread_pool")]
		[JsonFormatter(typeof(VerbatimDictionaryInterfaceKeysFormatter<string, NodeThreadPoolInfo>))]
		public Dictionary<string, NodeThreadPoolInfo> ThreadPool { get; internal set; }

		[DataMember(Name = "transport")]
		public NodeInfoTransport Transport { get; internal set; }

		[DataMember(Name = "transport_address")]
		public string TransportAddress { get; internal set; }

		[DataMember(Name = "version")]
		public string Version { get; internal set; }
	}

	[DataContract]
	public class NodeOperatingSystemInfo
	{
		[DataMember(Name = "arch")]
		public string Architecture { get; internal set; }

		[DataMember(Name = "available_processors")]
		public int AvailableProcessors { get; internal set; }

		[DataMember(Name = "cpu")]
		public NodeInfoOSCPU Cpu { get; internal set; }

		[DataMember(Name = "mem")]
		public NodeInfoMemory Mem { get; internal set; }

		[DataMember(Name = "name")]
		public string Name { get; internal set; }

		[DataMember(Name = "refresh_interval_in_millis")]
		public int RefreshInterval { get; internal set; }

		[DataMember(Name = "swap")]
		public NodeInfoMemory Swap { get; internal set; }

		[DataMember(Name = "version")]
		public string Version { get; internal set; }
	}

	[DataContract]
	public class NodeInfoOSCPU
	{
		[DataMember(Name = "cache_size")]
		public string CacheSize { get; internal set; }

		[DataMember(Name = "cache_size_in_bytes")]
		public int CacheSizeInBytes { get; internal set; }

		[DataMember(Name = "cores_per_socket")]
		public int CoresPerSocket { get; internal set; }

		[DataMember(Name = "mhz")]
		public int Mhz { get; internal set; }

		[DataMember(Name = "model")]
		public string Model { get; internal set; }

		[DataMember(Name = "total_cores")]
		public int TotalCores { get; internal set; }

		[DataMember(Name = "total_sockets")]
		public int TotalSockets { get; internal set; }

		[DataMember(Name = "vendor")]
		public string Vendor { get; internal set; }
	}

	[DataContract]
	public class NodeInfoMemory
	{
		[DataMember(Name = "total")]
		public string Total { get; internal set; }

		[DataMember(Name = "total_in_bytes")]
		public long TotalInBytes { get; internal set; }
	}

	[DataContract]
	public class NodeProcessInfo
	{
		[DataMember(Name = "id")]
		public long Id { get; internal set; }

		[DataMember(Name = "mlockall")]
		public bool MlockAll { get; internal set; }

		[DataMember(Name = "refresh_interval_in_millis")]
		public long RefreshIntervalInMilliseconds { get; internal set; }
	}

	[DataContract]
	public class NodeJvmInfo
	{
		[DataMember(Name = "gc_collectors")]
		public IEnumerable<string> GCCollectors { get; internal set; }

		[DataMember(Name = "mem")]
		public NodeInfoJVMMemory Memory { get; internal set; }

		[DataMember(Name = "memory_pools")]
		public IEnumerable<string> MemoryPools { get; internal set; }

		[DataMember(Name = "pid")]
		public int PID { get; internal set; }

		[DataMember(Name = "start_time_in_millis")]
		public long StartTime { get; internal set; }

		[DataMember(Name = "version")]
		public string Version { get; internal set; }

		[DataMember(Name = "vm_name")]
		public string VMName { get; internal set; }

		[DataMember(Name = "vm_vendor")]
		public string VMVendor { get; internal set; }

		[DataMember(Name = "vm_version")]
		public string VMVersion { get; internal set; }
	}

	[DataContract]
	public class NodeInfoJVMMemory
	{
		[DataMember(Name = "direct_max")]
		public string DirectMax { get; internal set; }

		[DataMember(Name = "direct_max_in_bytes")]
		public long DirectMaxInBytes { get; internal set; }

		[DataMember(Name = "heap_init")]
		public string HeapInit { get; internal set; }

		[DataMember(Name = "heap_init_in_bytes")]
		public long HeapInitInBytes { get; internal set; }

		[DataMember(Name = "heap_max")]
		public string HeapMax { get; internal set; }

		[DataMember(Name = "heap_max_in_bytes")]
		public long HeapMaxInBytes { get; internal set; }

		[DataMember(Name = "non_heap_init")]
		public string NonHeapInit { get; internal set; }

		[DataMember(Name = "non_heap_init_in_bytes")]
		public long NonHeapInitInBytes { get; internal set; }

		[DataMember(Name = "non_heap_max")]
		public string NonHeapMax { get; internal set; }

		[DataMember(Name = "non_heap_max_in_bytes")]
		public long NonHeapMaxInBytes { get; internal set; }
	}

	[DataContract]
	public class NodeThreadPoolInfo
	{
		[DataMember(Name = "keep_alive")]
		public string KeepAlive { get; internal set; }

		[DataMember(Name = "max")]
		public int? Max { get; internal set; }

		[DataMember(Name = "min")]
		public int? Min { get; internal set; }

		[DataMember(Name = "queue_size")]
		public int? QueueSize { get; internal set; }

		[DataMember(Name = "type")]
		public string Type { get; internal set; }
	}

	[DataContract]
	public class NodeInfoNetwork
	{
		[DataMember(Name = "primary_interface")]
		public NodeInfoNetworkInterface PrimaryInterface { get; internal set; }

		[DataMember(Name = "refresh_interval")]
		public int RefreshInterval { get; internal set; }
	}

	[DataContract]
	public class NodeInfoNetworkInterface
	{
		[DataMember(Name = "address")]
		public string Address { get; internal set; }

		[DataMember(Name = "mac_address")]
		public string MacAddress { get; internal set; }

		[DataMember(Name = "name")]
		public string Name { get; internal set; }
	}

	[DataContract]
	public class NodeInfoTransport
	{
		[DataMember(Name = "bound_address")]
		public IEnumerable<string> BoundAddress { get; internal set; }

		[DataMember(Name = "publish_address")]
		public string PublishAddress { get; internal set; }
	}

	[DataContract]
	public class NodeInfoHttp
	{
		[DataMember(Name = "bound_address")]
		public IEnumerable<string> BoundAddress { get; internal set; }

		[DataMember(Name = "max_content_length")]
		public string MaxContentLength { get; internal set; }

		[DataMember(Name = "max_content_length_in_bytes")]
		public long MaxContentLengthInBytes { get; internal set; }

		[DataMember(Name = "publish_address")]
		public string PublishAddress { get; internal set; }
	}
}
