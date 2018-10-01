using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class IndicesStats
	{
		/// <summary>
		/// Introduced in 6.4.0, returns the index UUID
		/// </summary>
		[JsonProperty("uuid")]
		public string Uuid { get; }

		[JsonProperty("primaries")]
		public IndexStats Primaries { get; internal set; }

		[JsonProperty("total")]
		public IndexStats Total { get; internal set; }

		[JsonProperty("shards")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, ShardStats[]>))]
		public IReadOnlyDictionary<string, ShardStats[]> Shards { get; internal set; } = EmptyReadOnly<string, ShardStats[]>.Dictionary;
	}
}
