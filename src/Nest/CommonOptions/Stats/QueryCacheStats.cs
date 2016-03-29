using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class QueryCacheStats
	{
		[JsonProperty("memory_size")]
		public string MemorySize { get; set; }
		[JsonProperty("memory_size_in_bytes")]
		public long MemorySizeInBytes { get; set; }

		[JsonProperty("total_count")]
		public long TotalCount { get; set; }

		[JsonProperty("hit_count")]
		public long HitCount { get; set; }

		[JsonProperty("miss_count")]
		public long MissCount { get; set; }

		[JsonProperty("cache_size")]
		public long CacheSize { get; set; }

		[JsonProperty("cache_count")]
		public long CacheCount { get; set; }

		[JsonProperty("evictions")]
		public long Evictions { get; set; }

	}
}