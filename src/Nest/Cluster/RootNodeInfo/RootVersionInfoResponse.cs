using Newtonsoft.Json;

namespace Nest
{
	public interface IRootNodeInfoResponse : IResponse
	{
		string Name { get; }
		string Tagline { get;  }
		ElasticsearchVersionInfo Version { get;  }
	}

	[JsonObject]
	public class RootNodeInfoResponse : ResponseBase, IRootNodeInfoResponse
	{
		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("tagline")]
		public string Tagline { get; internal set; }

		[JsonProperty("version")]
		public ElasticsearchVersionInfo Version { get; internal set; }

	}
}
