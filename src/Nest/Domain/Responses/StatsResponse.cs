using Newtonsoft.Json;

namespace Nest
{
    public interface IStatsResponse : IResponse
    {
        bool OK { get; }
        ShardsMetaData Shards { get; }
        Stats Stats { get; set; }
    }

    [JsonObject]
	public class StatsResponse : BaseResponse, IStatsResponse
    {
		public StatsResponse()
		{
			this.IsValid = true;
		}

		[JsonProperty(PropertyName = "ok")]
		public bool OK { get; internal set; }

		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData Shards { get; internal set; }

		[JsonProperty(PropertyName="_all")]
		public Stats Stats { get; set; } 
	}
}