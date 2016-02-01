using Newtonsoft.Json;

namespace Nest
{
	public interface IUnregisterPercolatorResponse : IResponse
	{
		bool Found { get; }
		string Index { get; }
		string Type { get; }
		string Id { get; }
		int Version { get; }
	}

	[JsonObject]
	public class UnregisterPercolatorResponse : ResponseBase, IUnregisterPercolatorResponse
	{
		[JsonProperty(PropertyName = "found")]
		public bool Found { get; internal set; }

		[JsonProperty(PropertyName = "_index")]
		public string Index { get; internal set; }

		[JsonProperty(PropertyName = "_type")]
		public string Type { get; internal set; }

		[JsonProperty(PropertyName = "_id")]
		public string Id { get; internal set; }

		[JsonProperty(PropertyName = "_version")]
		public int Version { get; internal set; }
	}
}
