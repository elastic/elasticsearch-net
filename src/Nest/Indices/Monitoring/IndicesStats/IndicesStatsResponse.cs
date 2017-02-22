using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public interface IIndicesStatsResponse : IResponse
	{
		ShardsMetaData Shards { get; }
		IndicesStats Stats { get;}
		IReadOnlyDictionary<string, IndicesStats> Indices { get; }
	}

	[JsonObject]
	public class IndicesStatsResponse : ResponseBase, IIndicesStatsResponse
	{

		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData Shards { get; internal set; }

		[JsonProperty(PropertyName = "_all")]
		public IndicesStats Stats { get; internal set; }

		[JsonProperty(PropertyName = "indices")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, IndicesStats>))]
		public IReadOnlyDictionary<string, IndicesStats> Indices { get; internal set; } = EmptyReadOnly<string, IndicesStats>.Dictionary;

	}
}
