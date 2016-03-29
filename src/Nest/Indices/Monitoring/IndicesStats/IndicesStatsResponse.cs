using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IIndicesStatsResponse : IResponse
	{
		ShardsMetaData Shards { get; }
		IndicesStats Stats { get; set; }
		Dictionary<string, IndicesStats> Indices { get; set; }
	}

	[JsonObject]
	public class IndicesStatsResponse : ResponseBase, IIndicesStatsResponse
	{

		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData Shards { get; internal set; }

		[JsonProperty(PropertyName = "_all")]
		public IndicesStats Stats { get; set; }

		[JsonProperty(PropertyName = "indices")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		public Dictionary<string, IndicesStats> Indices { get; set; }

	}
}