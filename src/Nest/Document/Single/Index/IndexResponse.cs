using Newtonsoft.Json;

namespace Nest
{
	public interface IIndexResponse : IResponse
	{
		string Id { get; }
		string Index { get; }
		string Type { get; }
		long Version { get; }
		bool Created { get; }
	}

	[JsonObject]
	public class IndexResponse : ResponseBase, IIndexResponse
	{
		[JsonProperty("_index")]
		public string Index { get; internal set; }
		[JsonProperty("_type")]
		public string Type { get; internal set; }
		[JsonProperty("_id")]
		public string Id { get; internal set; }
		[JsonProperty("_version")]
		public long Version { get; internal set; }
		[JsonProperty("created")]
		public bool Created { get; internal set; }

	}
}
