using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ShardQueryCache
	{
		[DataMember(Name ="cache_count")]
		public long CacheCount { get; internal set; }

		[DataMember(Name ="cache_size")]
		public long CacheSize { get; internal set; }

		[DataMember(Name ="evictions")]
		public long Evictions { get; internal set; }

		[DataMember(Name ="hit_count")]
		public long HitCount { get; internal set; }

		[DataMember(Name ="memory_size_in_bytes")]
		public long MemorySizeInBytes { get; internal set; }

		[DataMember(Name ="miss_count")]
		public long MissCount { get; internal set; }

		[DataMember(Name ="total_count")]
		public long TotalCount { get; internal set; }
	}
}
