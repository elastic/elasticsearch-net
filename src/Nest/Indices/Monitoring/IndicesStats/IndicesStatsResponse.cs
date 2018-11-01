using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IIndicesStatsResponse : IResponse
	{
		IReadOnlyDictionary<string, IndicesStats> Indices { get; }
		ShardStatistics Shards { get; }
		IndicesStats Stats { get; }
	}

	[JsonObject]
	public class IndicesStatsResponse : ResponseBase, IIndicesStatsResponse
	{
		[JsonProperty("indices")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, IndicesStats>))]
		public IReadOnlyDictionary<string, IndicesStats> Indices { get; internal set; } = EmptyReadOnly<string, IndicesStats>.Dictionary;

		[JsonProperty("_shards")]
		public ShardStatistics Shards { get; internal set; }

		[JsonProperty("_all")]
		public IndicesStats Stats { get; internal set; }
	}
}
