using Newtonsoft.Json;

namespace Nest
{
	public interface IIndicesResponse : IResponse
	{
		bool Acknowledged { get; }
		ShardsMetaData ShardsHit { get; }
	}

	[JsonObject]
	public class IndicesResponse : BaseResponse, IIndicesResponse
	{
		[JsonProperty(PropertyName = "acknowledged")]
		public bool Acknowledged { get; private set; }

		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData ShardsHit { get; private set; }
	}
}
