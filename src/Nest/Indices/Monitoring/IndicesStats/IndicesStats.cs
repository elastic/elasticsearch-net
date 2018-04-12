using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class IndicesStats
	{
		[JsonProperty("primaries")]
		public IndexStats Primaries { get; set; }

		[JsonProperty("total")]
		public IndexStats Total { get; set; }

		[JsonProperty("shards")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, IndicesStats[]>))]
		public IReadOnlyDictionary<string, ShardStats[]> Shards { get; set; } = EmptyReadOnly<string, ShardStats[]>.Dictionary;
	}
}
