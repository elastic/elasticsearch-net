using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardRequestCache
	{
		[JsonProperty("memory_size_in_bytes")]
		public long MemorySizeInBytes { get; set; }
		[JsonProperty("evictions")]
		public long Evictions { get; set; }
		[JsonProperty("hit_count")]
		public long HitCount { get; set; }
		[JsonProperty("miss_count")]
		public long MissCount { get; set; }
	}
}