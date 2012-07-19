using Newtonsoft.Json;

namespace Nest
{
    public interface ICountResponse : IResponse
    {
        int Count { get; }
        ShardsMetaData Shards { get; }
    }

    [JsonObject]
	public class CountResponse : BaseResponse, ICountResponse
    {
		public CountResponse()
		{
			this.IsValid = true;
		}

		[JsonProperty(PropertyName = "count")]
		public int Count { get; internal set; }

		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData Shards { get; internal set; }
	}
}