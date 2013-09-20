using Newtonsoft.Json;

namespace Nest
{
	public interface IRootInfoResponse : IResponse
	{
		bool OK { get; }
		string Name { get; }
		string Tagline { get;  }
		ElasticSearchVersionInfo Version { get;  }
	}

	[JsonObject]
	public class RootInfoResponse : BaseResponse, IRootInfoResponse
	{
		public RootInfoResponse()
		{
			this.IsValid = true;
		}

		[JsonProperty(PropertyName = "ok")]
		public bool OK { get; internal set; }

		[JsonProperty(PropertyName = "name")]
		public string Name { get; internal set; }

		[JsonProperty(PropertyName = "tagline")]
		public string Tagline { get; internal set; }
		
		[JsonProperty(PropertyName = "version")]
		public ElasticSearchVersionInfo Version { get; internal set; }

	}
}