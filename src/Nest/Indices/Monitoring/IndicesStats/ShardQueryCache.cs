using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardQueryCache
	{
		[JsonProperty("memory_size_in_bytes")]
		public long MemorySizeInBytes { get; internal set; }
		[JsonProperty("total_count")]
		public long TotalCount { get; internal set; }
		[JsonProperty("hit_count")]
		public long HitCount { get; internal set; }
		[JsonProperty("miss_count")]
		public long MissCount { get; internal set; }
		[JsonProperty("cache_size")]
		public long CacheSize { get; internal set; }
		[JsonProperty("cache_count")]
		public long CacheCount { get; internal set; }
		[JsonProperty("evictions")]
		public long Evictions { get; internal set; }
	}
}
