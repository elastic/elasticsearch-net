using Newtonsoft.Json;

namespace Nest
{
	public interface IUpdateResponse : IResponse
	{
		ShardsMetaData ShardsHit { get; }
		string Index { get; }
		string Type { get; }
		string Id { get; }
		string Version { get; }
	}

	[JsonObject]
	public class UpdateResponse : BaseResponse, IUpdateResponse
	{
		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData ShardsHit { get; private set; }

		[JsonProperty(PropertyName = "_index")]
		public string Index { get; private set; }
		[JsonProperty(PropertyName = "_type")]
		public string Type { get; private set; }
		[JsonProperty(PropertyName = "_id")]
		public string Id { get; private set; }
		[JsonProperty(PropertyName = "_version")]
		public string Version { get; private set; }
	}
}
