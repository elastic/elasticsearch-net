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
		[JsonProperty(PropertyName = "name")]
		public string Name { get; internal set; }

		[JsonProperty(PropertyName = "tagline")]
		public string Tagline { get; internal set; }
		
		[JsonProperty(PropertyName = "version")]
		public ElasticsearchVersionInfo Version { get; internal set; }

	}
}