using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ShardRequestCache
	{
		[DataMember(Name ="evictions")]
		public long Evictions { get; internal set; }

		[DataMember(Name ="hit_count")]
		public long HitCount { get; internal set; }

		[DataMember(Name ="memory_size_in_bytes")]
		public long MemorySizeInBytes { get; internal set; }

		[DataMember(Name ="miss_count")]
		public long MissCount { get; internal set; }
	}
}
