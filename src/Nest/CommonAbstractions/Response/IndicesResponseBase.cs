using Newtonsoft.Json;

namespace Nest
{
	public interface IIndicesResponse : IResponse
	{
		bool Acknowledged { get; }
		ShardsMetadata ShardsHit { get; }
	}

	[JsonObject]
	public abstract class IndicesResponseBase : ResponseBase, IIndicesResponse
	{
		[JsonProperty("acknowledged")]
		public bool Acknowledged { get; private set; }

		[JsonProperty("_shards")]
		public ShardsMetadata ShardsHit { get; private set; }
	}
}
