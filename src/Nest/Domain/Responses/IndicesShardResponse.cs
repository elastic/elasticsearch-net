using Newtonsoft.Json;

namespace Nest
{
    public interface IIndicesShardResponse : IResponse
    {
        bool OK { get; }
        ShardsMetaData Shards { get; }
    }

    [JsonObject]
	public class IndicesShardResponse : BaseResponse, IIndicesShardResponse
    {
		public IndicesShardResponse()
		{
			this.IsValid = true;
		}

		[JsonProperty(PropertyName = "ok")]
		public bool OK { get; internal set; }

		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData Shards { get; internal set; }
	}
}