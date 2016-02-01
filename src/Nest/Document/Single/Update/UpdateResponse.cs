using Newtonsoft.Json;

namespace Nest
{
	public interface IUpdateResponse<T> : IResponse
		where T : class
	{
		[JsonProperty(PropertyName = "_shards")]
		ShardsMetaData ShardsHit { get; }

		[JsonProperty(PropertyName = "_index")]
		string Index { get; }

		[JsonProperty(PropertyName = "_type")]
		string Type { get; }

		[JsonProperty(PropertyName = "_id")]
		string Id { get; }

		[JsonProperty(PropertyName = "_version")]
		long Version { get; }

		[JsonProperty(PropertyName = "get")]
		InstantGet<T> Get { get; }
	}

	[JsonObject]
	public class UpdateResponse<T> : ResponseBase, IUpdateResponse<T>
		where T : class
	{
		public ShardsMetaData ShardsHit { get; private set; }
		public string Index { get; private set; }
		public string Type { get; private set; }
		public string Id { get; private set; }
		public long Version { get; private set; }
		public InstantGet<T> Get { get; private set; }
	}
}
