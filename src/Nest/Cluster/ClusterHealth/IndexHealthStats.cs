using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class IndexHealthStats
	{
		[JsonProperty(PropertyName = "status")]
		public string Status { get; set; }

		[JsonProperty(PropertyName = "number_of_shards")]
		public int NumberOfShards { get; set; }
		[JsonProperty(PropertyName = "number_of_replicas")]
		public int NumberOfReplicas { get; set; }

		[JsonProperty(PropertyName = "active_primary_shards")]
		public int ActivePrimaryShards { get; set; }
		[JsonProperty(PropertyName = "active_shards")]
		public int ActiveShards { get; set; }
		[JsonProperty(PropertyName = "relocating_shards")]
		public int RelocatingShards { get; set; }
		[JsonProperty(PropertyName = "initializing_shards")]
		public int InitializingShards { get; set; }
		[JsonProperty(PropertyName = "unassigned_shards")]
		public int UnassignedShards { get; set; }

		[JsonProperty(PropertyName = "shards")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		public Dictionary<string, ShardHealthStats> Shards { get; set; }
	}
}
