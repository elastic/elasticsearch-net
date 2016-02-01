using Newtonsoft.Json;

namespace Nest
{
	public interface ICountResponse : IResponse
	{
		long Count { get; }
		ShardsMetaData Shards { get; }
	}

	[JsonObject]
	public class CountResponse : ResponseBase, ICountResponse
	{
		[JsonProperty(PropertyName = "count")]
		public long Count { get; internal set; }

		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData Shards { get; internal set; }
	}
}