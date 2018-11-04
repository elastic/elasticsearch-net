using Newtonsoft.Json;

namespace Nest
{
	public interface IIndexResponse : IResponse
	{
		bool Created { get; }
		string Id { get; }
		string Index { get; }
		Result Result { get; }
		string Type { get; }
		long Version { get; }
	}

	[JsonObject]
	public class IndexResponse : ResponseBase, IIndexResponse
	{
		[JsonProperty("created")]
		public bool Created { get; internal set; }

		[JsonProperty("_id")]
		public string Id { get; internal set; }

		[JsonProperty("_index")]
		public string Index { get; internal set; }

		[JsonProperty("result")]
		public Result Result { get; internal set; }

		[JsonProperty("_type")]
		public string Type { get; internal set; }

		[JsonProperty("_version")]
		public long Version { get; internal set; }
	}
}
