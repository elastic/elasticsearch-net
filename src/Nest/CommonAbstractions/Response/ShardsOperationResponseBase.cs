using Newtonsoft.Json;

namespace Nest
{
	public interface IShardsOperationResponse : IResponse
	{
		ShardStatistics Shards { get; }
	}

	public abstract class ShardsOperationResponseBase : ResponseBase, IShardsOperationResponse
	{
		[JsonProperty("_shards")]
		public ShardStatistics Shards { get; internal set; }
	}
}
