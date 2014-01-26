using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
    public interface IGlobalStatsResponse : IResponse
    {
        bool OK { get; }
        ShardsMetaData Shards { get; }
        Stats Stats { get; set; }
		Dictionary<string, Stats> Indices { get; set; }
    }

    [JsonObject]
	public class GlobalStatsResponse : BaseResponse, IGlobalStatsResponse
    {
		public GlobalStatsResponse()
		{
			this.IsValid = true;
		}

		[JsonProperty(PropertyName = "ok")]
		public bool OK { get; internal set; }

		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData Shards { get; internal set; }

		[JsonProperty(PropertyName = "_all")]
		public Stats Stats { get; set; }

		[JsonProperty(PropertyName = "indices")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public Dictionary<string, Stats> Indices { get; set; }

	}
}