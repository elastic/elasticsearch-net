using Newtonsoft.Json;

namespace Nest
{
	public interface IUpgradeResponse : IResponse
	{
		[JsonProperty("_shards")]
		ShardsMetadata Shards { get; }
	}

	public class UpgradeResponse : ResponseBase, IUpgradeResponse
	{
		public ShardsMetadata Shards { get; internal set; }
	}
}
