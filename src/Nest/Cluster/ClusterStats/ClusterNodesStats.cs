using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ClusterNodesStats
	{
		[DataMember(Name ="count")]
		public ClusterNodeCount Count { get; internal set; }

		[DataMember(Name ="fs")]
		public ClusterFileSystem FileSystem { get; internal set; }

		[DataMember(Name ="jvm")]
		public ClusterJvm Jvm { get; internal set; }

		[DataMember(Name ="os")]
		public ClusterOperatingSystemStats OperatingSystem { get; internal set; }

		[DataMember(Name ="plugins")]
		public IReadOnlyCollection<PluginStats> Plugins { get; internal set; }

		[DataMember(Name ="process")]
		public ClusterProcess Process { get; internal set; }

		[DataMember(Name ="versions")]
		public IReadOnlyCollection<string> Versions { get; internal set; }
	}

	[DataContract]
	public class ClusterFileSystem
	{
		[DataMember(Name ="available")]
		public string Available { get; internal set; }

		[DataMember(Name ="available_in_bytes")]
		public long AvailableInBytes { get; internal set; }

		[DataMember(Name ="free")]
		public string Free { get; internal set; }

		[DataMember(Name ="free_in_bytes")]
		public long FreeInBytes { get; internal set; }

		[DataMember(Name ="total")]
		public string Total { get; internal set; }

		[DataMember(Name ="total_in_bytes")]
		public long TotalInBytes { get; internal set; }
	}

	[DataContract]
	public class ClusterJvm
	{
		[DataMember(Name ="max_uptime")]
		public string MaxUptime { get; internal set; }

		[DataMember(Name ="max_uptime_in_millis")]
		public long MaxUptimeInMilliseconds { get; internal set; }

		[DataMember(Name ="mem")]
		public ClusterJvmMemory Memory { get; internal set; }

		[DataMember(Name ="threads")]
		public long Threads { get; internal set; }

		[DataMember(Name ="versions")]
		public IReadOnlyCollection<ClusterJvmVersion> Versions { get; internal set; }
	}

	[DataContract]
	public class ClusterJvmVersion
	{
		[DataMember(Name ="count")]
		public int Count { get; internal set; }

		[DataMember(Name ="version")]
		public string Version { get; internal set; }

		[DataMember(Name ="vm_name")]
		public string VmName { get; internal set; }

		[DataMember(Name ="vm_vendor")]
		public string VmVendor { get; internal set; }

		[DataMember(Name ="vm_version")]
		public string VmVersion { get; internal set; }
	}

	[DataContract]
	public class ClusterJvmMemory
	{
		[DataMember(Name ="heap_max")]
		public string HeapMax { get; internal set; }

		[DataMember(Name ="heap_max_in_bytes")]
		public long HeapMaxInBytes { get; internal set; }

		[DataMember(Name ="heap_used")]
		public string HeapUsed { get; set; }

		[DataMember(Name ="heap_used_in_bytes")]
		public long HeapUsedInBytes { get; internal set; }
	}

	[DataContract]
	public class ClusterProcess
	{
		[DataMember(Name ="cpu")]
		public ClusterProcessCpu Cpu { get; internal set; }

		[DataMember(Name ="open_file_descriptors")]
		public ClusterProcessOpenFileDescriptors OpenFileDescriptors { get; internal set; }
	}

	[DataContract]
	public class ClusterProcessCpu
	{
		[DataMember(Name ="percent")]
		public int Percent { get; internal set; }
	}

	[DataContract]
	public class ClusterProcessOpenFileDescriptors
	{
		[DataMember(Name ="avg")]
		public long Avg { get; internal set; }

		[DataMember(Name ="max")]
		public long Max { get; internal set; }

		[DataMember(Name ="min")]
		public long Min { get; internal set; }
	}

	[DataContract]
	public class ClusterOperatingSystemStats
	{
		[DataMember(Name ="allocated_processors")]
		public int AllocatedProcessors { get; internal set; }

		[DataMember(Name ="available_processors")]
		public int AvailableProcessors { get; internal set; }

		[DataMember(Name ="names")]
		public IReadOnlyCollection<ClusterOperatingSystemName> Names { get; internal set; }
	}

	[DataContract]
	public class ClusterOperatingSystemName
	{
		[DataMember(Name ="count")]
		public int Count { get; internal set; }

		[DataMember(Name ="name")]
		public string Name { get; internal set; }
	}

	[DataContract]
	public class ClusterNodeCount
	{
		[DataMember(Name ="coordinating_only")]
		public int CoordinatingOnly { get; internal set; }

		[DataMember(Name ="data")]
		public int Data { get; internal set; }

		[DataMember(Name ="ingest")]
		public int Ingest { get; internal set; }

		[DataMember(Name ="master")]
		public int Master { get; internal set; }

		[DataMember(Name ="total")]
		public int Total { get; internal set; }
	}
}
