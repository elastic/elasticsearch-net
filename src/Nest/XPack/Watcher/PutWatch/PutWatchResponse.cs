using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<PutWatchResponse>))]
	public interface IPutWatchResponse : IResponse
	{
		[JsonProperty("_id")]
		string Id { get; }

		[JsonProperty("_version")]
		int Version { get; }

		[JsonProperty("created")]
		bool Created { get; }
	}

	public class PutWatchResponse : ResponseBase, IPutWatchResponse
	{
		public string Id { get; internal set; }

		public int Version { get; internal set; }

		public bool Created { get; internal set; }
	}
}
