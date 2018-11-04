using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class IndexHealthStats
	{
		[JsonProperty(PropertyName = "active_primary_shards")]
		public int ActivePrimaryShards { get; internal set; }

		[JsonProperty(PropertyName = "active_shards")]
		public int ActiveShards { get; internal set; }

		[JsonProperty(PropertyName = "initializing_shards")]
		public int InitializingShards { get; internal set; }

		[JsonProperty(PropertyName = "number_of_replicas")]
		public int NumberOfReplicas { get; internal set; }

		[JsonProperty(PropertyName = "number_of_shards")]
		public int NumberOfShards { get; internal set; }

		[JsonProperty(PropertyName = "relocating_shards")]
		public int RelocatingShards { get; internal set; }

		[JsonProperty(PropertyName = "shards")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, ShardHealthStats>))]
		public IReadOnlyDictionary<string, ShardHealthStats> Shards { get; internal set; } =
			EmptyReadOnly<string, ShardHealthStats>.Dictionary;

		[JsonProperty(PropertyName = "status")]
		public string Status { get; internal set; }

		[JsonProperty(PropertyName = "unassigned_shards")]
		public int UnassignedShards { get; internal set; }
	}
}
