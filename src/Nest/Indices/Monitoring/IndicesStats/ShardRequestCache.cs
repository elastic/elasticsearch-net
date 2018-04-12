using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardRequestCache
	{
		[JsonProperty("memory_size_in_bytes")]
		public long MemorySizeInBytes { get; internal set; }
		[JsonProperty("evictions")]
		public long Evictions { get; internal set; }
		[JsonProperty("hit_count")]
		public long HitCount { get; internal set; }
		[JsonProperty("miss_count")]
		public long MissCount { get; internal set; }
	}
}
