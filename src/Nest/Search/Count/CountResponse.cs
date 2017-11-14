using Newtonsoft.Json;

namespace Nest
{
	public interface ICountResponse : IResponse
	{
		long Count { get; }
		ShardsMetadata Shards { get; }
	}

	[JsonObject]
	public class CountResponse : ResponseBase, ICountResponse
	{
		[JsonProperty("count")]
		public long Count { get; internal set; }

		[JsonProperty("_shards")]
		public ShardsMetadata Shards { get; internal set; }
	}
}
