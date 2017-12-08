using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IIndicesStatsResponse : IResponse
	{
		ShardStatistics Shards { get; }
		IndicesStats Stats { get;}
		IReadOnlyDictionary<string, IndicesStats> Indices { get; }
	}

	[JsonObject]
	public class IndicesStatsResponse : ResponseBase, IIndicesStatsResponse
	{

		[JsonProperty("_shards")]
		public ShardStatistics Shards { get; internal set; }

		[JsonProperty("_all")]
		public IndicesStats Stats { get; internal set; }

		[JsonProperty("indices")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, IndicesStats>))]
		public IReadOnlyDictionary<string, IndicesStats> Indices { get; internal set; } = EmptyReadOnly<string, IndicesStats>.Dictionary;

	}
}
