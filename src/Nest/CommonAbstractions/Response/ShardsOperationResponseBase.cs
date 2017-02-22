using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public interface IShardsOperationResponse : IResponse
	{
		ShardsMetaData Shards { get; }
	}

	public abstract class ShardsOperationResponseBase : ResponseBase, IShardsOperationResponse
	{
		[JsonProperty("_shards")]
		public ShardsMetaData Shards { get; internal set; }
	}
}