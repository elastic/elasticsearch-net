using Newtonsoft.Json;

namespace Nest
{
	public interface IUpdateResponse<TDocument> : IResponse
		where TDocument : class
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
		InstantGet<TDocument> Get { get; }

		[JsonProperty("result")]
		Result Result { get; }
	}

	[JsonObject]
	public class UpdateResponse<TDocument> : ResponseBase, IUpdateResponse<TDocument>
		where TDocument : class
	{
		public ShardsMetaData ShardsHit { get; internal set; }
		public string Index { get; internal set; }
		public string Type { get; internal set; }
		public string Id { get; internal set; }
		public long Version { get; internal set; }
		public InstantGet<TDocument> Get { get; internal set; }
		public Result Result { get; internal set; }
	}
}
