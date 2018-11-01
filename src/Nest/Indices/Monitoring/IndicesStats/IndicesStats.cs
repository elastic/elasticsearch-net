using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class IndicesStats
	{
		[JsonProperty("primaries")]
		public IndexStats Primaries { get; internal set; }

		[JsonProperty("shards")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, ShardStats[]>))]
		public IReadOnlyDictionary<string, ShardStats[]> Shards { get; internal set; } = EmptyReadOnly<string, ShardStats[]>.Dictionary;

		[JsonProperty("total")]
		public IndexStats Total { get; internal set; }

		/// <summary>
		///     Universal Unique Identifier for the index
		/// </summary>
		/// <remarks>
		///     Introduced in Elasticsearch 6.4.0
		/// </remarks>
		[JsonProperty("uuid")]
		public string UUID { get; }
	}
}
