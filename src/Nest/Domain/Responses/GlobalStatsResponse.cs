using Newtonsoft.Json;

namespace Nest
{
    public interface IGlobalStatsResponse : IResponse
    {
        bool OK { get; }
        ShardsMetaData Shards { get; }
        GlobalStats Stats { get; set; }
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
		public GlobalStats Stats { get; set; }

	}
}