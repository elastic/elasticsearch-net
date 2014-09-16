using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject]
	public class ClusterNodesStats
	{
		[JsonProperty("count")]
		public ClusterNodeCount Count { get; internal set; }

		[JsonProperty("versions")]
		public List<string> Versions { get; internal set; }

		[JsonProperty("os")]
		public ClusterOs Os { get; internal set; }

		[JsonProperty("process")]
		public ClusterProcess Process { get; internal set; }

		[JsonProperty("jvm")]
		public ClusterJvm Jvm { get; internal set; }

		[JsonProperty("fs")]
		public ClusterFileSystem FileSystem { get; internal set; }
		
		[JsonProperty("plugins")]
		public List<PluginStats> Plugins { get; internal set; } 
	}

	[JsonObject]
	public class ClusterFileSystem
	{
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
		[JsonProperty("disk_io_op")]
		public long DiskIoOps { get; internal set; }
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

	[JsonObject]
	public class ClusterJvm
	{
		[JsonProperty("max_uptime")]
		public string MaxUptime { get; internal set; }

		[JsonProperty("max_uptime_in_milliseconds")]
		public string MaxUptimeInMilliseconds { get; internal set; }

		[JsonProperty("versions")]
		public List<ClusterJvmVersion> Versions { get; internal set; }

		[JsonProperty("mem")]
		public ClusterJvmMemory Memory { get; internal set; }

		[JsonProperty("threads")]
		public long Threads { get; internal set; }
	}

	[JsonObject]
	public class ClusterJvmVersion
	{
		[JsonProperty("version")]
		public string Version { get; internal set; }

		[JsonProperty("vm_name")]
		public string VmName { get; internal set; }

		[JsonProperty("vm_version")]
		public string VmVersion { get; internal set; }

		[JsonProperty("vm_vendor")]
		public string VmVendor { get; internal set; }

		[JsonProperty("count")]
		public int Count { get; internal set; }	
	}

	[JsonObject]
	public class ClusterJvmMemory
	{
		[JsonProperty("heap_used")]
		public string HeapUsed { get ;set;}

		[JsonProperty("heap_used_in_bytes")]
		public long HeapUsedInBytes { get; internal set; }

		[JsonProperty("heap_max")]
		public string HeapMax { get; internal set; }

		[JsonProperty("heap_max_in_bytes")]
		public long HeapMaxInBytes { get; internal set; }
	}

	[JsonObject]
	public class ClusterProcess
	{
		[JsonProperty("cpu")]
		public ClusterProcessCpu Cpu { get; internal set; }

		[JsonProperty("open_file_descriptors")]
		public ClusterProcessOpenFileDescriptors OpenFileDescriptors { get; internal set; }
	}

	[JsonObject]
	public class ClusterProcessCpu
	{
		[JsonProperty("percent")]
		public int Percent { get; internal set; }
	}

	[JsonObject]
	public class ClusterProcessOpenFileDescriptors
	{
		[JsonProperty("min")]
		public long Min { get; internal set; }

		[JsonProperty("max")]
		public long Max { get; internal set; }

		[JsonProperty("avg")]
		public long Avg { get; internal set; }
	}

	[JsonObject]
	public class ClusterOs
	{
		[JsonProperty("available_processors")]
		public int AvailableProcessors { get; internal set; }

		[JsonProperty("mem")]
		public ClusterOsMemory Memory { get; internal set; }

		[JsonProperty("cpu")]
		public List<ClusterCpu> Cpu { get; internal set; }
	}

	[JsonObject]
	public class ClusterOsMemory
	{
		[JsonProperty("total_in_bytes")]
		public long TotalInBytes { get; internal set; }
	}

	[JsonObject]
	public class ClusterCpu
	{
		[JsonProperty("vendor")]
		public string Vendor { get; internal set; }

		[JsonProperty("model")]
		public string Model { get; internal set; }

		[JsonProperty("mhz")]
		public int Mhz { get; internal set; }

		[JsonProperty("total_scores")]
		public int TotalCores { get; internal set; }

		[JsonProperty("total_sockets")]
		public int TotalSockets { get; internal set; }

		[JsonProperty("cores_per_socket")]
		public int CoresPerSocket { get; internal set; }

		[JsonProperty("cache_size_in_bytes")]
		public int CacheSizeInBytes { get; internal set; }

		[JsonProperty("count")]
		public int Count { get; internal set; }
	}

	[JsonObject]
	public class ClusterNodeCount
	{
		[JsonProperty("total")]
		public int Total { get; internal set; }

		[JsonProperty("master_only")]
		public int MasterOnly { get; internal set; }

		[JsonProperty("data_only")]
		public int DataOnly { get; internal set; }

		[JsonProperty("master_data")]
		public int MasterData { get; internal set; }

		[JsonProperty("client")]
		public int Client { get; internal set; }
	}
}
