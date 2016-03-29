using Newtonsoft.Json;

namespace Nest
{
	public interface IIndicesResponse : IResponse
	{
		bool Acknowledged { get; }
		ShardsMetaData ShardsHit { get; }
	}

	[JsonObject]
	public abstract class IndicesResponseBase : ResponseBase, IIndicesResponse
	{
		[JsonProperty(PropertyName = "acknowledged")]
		public bool Acknowledged { get; private set; }

		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData ShardsHit { get; private set; }
	}
}
