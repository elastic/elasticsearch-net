using Newtonsoft.Json;

namespace Nest
{
	public interface IShardsOperationResponse : IResponse
	{
		ShardsMetadata Shards { get; }
	}

	public abstract class ShardsOperationResponseBase : ResponseBase, IShardsOperationResponse
	{
		[JsonProperty("_shards")]
		public ShardsMetadata Shards { get; internal set; }
	}
}
