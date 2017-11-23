using Newtonsoft.Json;

namespace Nest
{
	public interface IUpdateResponse<T> : IResponse
		where T : class
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
		InstantGet<T> Get { get; }

		[JsonProperty("result")]
		Result Result { get; }
	}

	[JsonObject]
	public class UpdateResponse<T> : ResponseBase, IUpdateResponse<T>
		where T : class
	{
		public ShardsMetadata ShardsHit { get; private set; }
		public string Index { get; private set; }
		public string Type { get; private set; }
		public string Id { get; private set; }
		public long Version { get; private set; }
		public InstantGet<T> Get { get; private set; }
		public Result Result { get; private set; }
	}
}
