using Newtonsoft.Json;

namespace Nest
{
	public interface IUpdateResponse<TDocument> : IResponse
		where TDocument : class
	{
		[JsonProperty("get")]
		InstantGet<TDocument> Get { get; }

		[JsonProperty("_id")]
		string Id { get; }

		[JsonProperty("_index")]
		string Index { get; }

		[JsonProperty("result")]
		Result Result { get; }

		[JsonProperty("_shards")]
		ShardStatistics ShardsHit { get; }

		[JsonProperty("_type")]
		string Type { get; }

		[JsonProperty("_version")]
		long Version { get; }
	}

	[JsonObject]
	public class UpdateResponse<TDocument> : ResponseBase, IUpdateResponse<TDocument>
		where TDocument : class
	{
		public InstantGet<TDocument> Get { get; internal set; }
		public string Id { get; internal set; }
		public string Index { get; internal set; }
		public Result Result { get; internal set; }
		public ShardStatistics ShardsHit { get; internal set; }
		public string Type { get; internal set; }
		public long Version { get; internal set; }
	}
}
