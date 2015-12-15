using System.Collections.Generic;
using Newtonsoft.Json;

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
		public ClusterOperatingSystemStats OperatingSystem { get; internal set; }

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
	}

	[JsonObject]
	public class ClusterJvm
	{
		[JsonProperty("max_uptime")]
		public string MaxUptime { get; internal set; }

		[JsonProperty("max_uptime_in_millis")]
		public long MaxUptimeInMilliseconds { get; internal set; }

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
	public class ClusterOperatingSystemStats
	{
		[JsonProperty("available_processors")]
		public int AvailableProcessors { get; internal set; }

		[JsonProperty("mem")]
		public ClusterOperatingSystemMemory Memory { get; internal set; }

		[JsonProperty("names")]
		public List<ClusterOperatingSystemName> Names { get; internal set; }
	}

	[JsonObject]
	public class ClusterOperatingSystemMemory
	{
		[JsonProperty("total")]
		public string Total { get; internal set; }
		[JsonProperty("total_in_bytes")]
		public long TotalInBytes { get; internal set; }
	}

	[JsonObject]
	public class ClusterOperatingSystemName
	{
		[JsonProperty("count")]
		public int Count { get; internal set; }

		[JsonProperty("name")]
		public string Name { get; internal set; }
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
