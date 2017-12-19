using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class IndexHealthStats
	{
		[JsonProperty("status")]
		public Health Status { get; internal set; }

		[JsonProperty("number_of_shards")]
		public int NumberOfShards { get; internal set; }
		[JsonProperty("number_of_replicas")]
		public int NumberOfReplicas { get; internal set; }

		[JsonProperty("active_primary_shards")]
		public int ActivePrimaryShards { get; internal set; }
		[JsonProperty("active_shards")]
		public int ActiveShards { get; internal set; }
		[JsonProperty("relocating_shards")]
		public int RelocatingShards { get; internal set; }
		[JsonProperty("initializing_shards")]
		public int InitializingShards { get; internal set; }
		[JsonProperty("unassigned_shards")]
		public int UnassignedShards { get; internal set; }

		[JsonProperty("shards")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, ShardHealthStats>))]
		public IReadOnlyDictionary<string, ShardHealthStats> Shards { get; internal set; } = EmptyReadOnly<string, ShardHealthStats>.Dictionary;
	}
}
