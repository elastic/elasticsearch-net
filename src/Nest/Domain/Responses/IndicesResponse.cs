using Newtonsoft.Json;

namespace Nest
{
    public interface IIndicesResponse : IResponse
    {
        bool OK { get; }
        ShardsMetaData ShardsHit { get; }
    }

    [JsonObject]
	public class IndicesResponse : BaseResponse, IIndicesResponse
    {
		[JsonProperty(PropertyName = "ok")]
		public bool OK { get; private set; }

		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData ShardsHit { get; private set; }
	}
}
