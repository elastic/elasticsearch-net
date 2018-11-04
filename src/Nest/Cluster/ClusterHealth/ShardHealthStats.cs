using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardHealthStats
	{
		[JsonProperty(PropertyName = "active_shards")]
		public int ActiveShards { get; internal set; }

		[JsonProperty(PropertyName = "initializing_shards")]
		public int InitializingShards { get; internal set; }

		[JsonProperty(PropertyName = "primary_active")]
		public bool PrimaryActive { get; internal set; }

		[JsonProperty(PropertyName = "relocating_shards")]
		public int RelocatingShards { get; internal set; }

		[JsonProperty(PropertyName = "status")]
		public string Status { get; internal set; }

		[JsonProperty(PropertyName = "unassigned_shards")]
		public int UnassignedShards { get; internal set; }
	}
}
