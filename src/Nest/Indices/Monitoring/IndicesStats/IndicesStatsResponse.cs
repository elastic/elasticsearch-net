using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IIndicesStatsResponse : IResponse
	{
		IReadOnlyDictionary<string, IndicesStats> Indices { get; }
		ShardsMetaData Shards { get; }
		IndicesStats Stats { get; }
	}

	[JsonObject]
	public class IndicesStatsResponse : ResponseBase, IIndicesStatsResponse
	{
		[JsonProperty(PropertyName = "indices")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, IndicesStats>))]
		public IReadOnlyDictionary<string, IndicesStats> Indices { get; internal set; } = EmptyReadOnly<string, IndicesStats>.Dictionary;

		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData Shards { get; internal set; }

		[JsonProperty(PropertyName = "_all")]
		public IndicesStats Stats { get; internal set; }
	}
}
