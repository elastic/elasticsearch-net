using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
	public interface IIndicesStatsResponse : IResponse
	{
		ShardsMetaData Shards { get; }
		Stats Stats { get; set; }
		Dictionary<string, Stats> Indices { get; set; }
	}

	[JsonObject]
	public class IndicesStatsResponse : BaseResponse, IIndicesStatsResponse
	{

		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData Shards { get; internal set; }

		[JsonProperty(PropertyName = "_all")]
		public Stats Stats { get; set; }

		[JsonProperty(PropertyName = "indices")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		public Dictionary<string, Stats> Indices { get; set; }

	}
}