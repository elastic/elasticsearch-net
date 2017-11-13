using Newtonsoft.Json;

namespace Nest
{
	public interface IUpdateResponse<TDocument> : IResponse
		where TDocument : class
	{
		[JsonProperty("_shards")]
		ShardsMetadata ShardsHit { get; }

		[JsonProperty("_index")]
		string Index { get; }

		[JsonProperty("_type")]
		string Type { get; }

		[JsonProperty("_id")]
		string Id { get; }

		[JsonProperty("_version")]
		long Version { get; }

		[JsonProperty("get")]
		InstantGet<TDocument> Get { get; }

		[JsonProperty("result")]
		Result Result { get; }
	}

	[JsonObject]
	public class UpdateResponse<TDocument> : ResponseBase, IUpdateResponse<TDocument>
		where TDocument : class
	{
		public ShardsMetadata ShardsHit { get; internal set; }
		public string Index { get; internal set; }
		public string Type { get; internal set; }
		public string Id { get; internal set; }
		public long Version { get; internal set; }
		public InstantGet<TDocument> Get { get; internal set; }
		public Result Result { get; internal set; }
	}
}
