using Newtonsoft.Json;

namespace Nest
{
	public interface IUpgradeResponse : IResponse
	{
		[JsonProperty("_shards")]
		ShardStatistics Shards { get; }
	}

	public class UpgradeResponse : ResponseBase, IUpgradeResponse
	{
		public ShardStatistics Shards { get; internal set; }
	}
}
