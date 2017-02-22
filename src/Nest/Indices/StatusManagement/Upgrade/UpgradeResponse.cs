using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public interface IUpgradeResponse : IResponse
	{
		[JsonProperty("_shards")]
		ShardsMetaData Shards { get; }
	}

	public class UpgradeResponse : ResponseBase, IUpgradeResponse
	{
		public ShardsMetaData Shards { get; internal set; }
	}
}
