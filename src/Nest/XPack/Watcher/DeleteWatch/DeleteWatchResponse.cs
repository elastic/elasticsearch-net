using Newtonsoft.Json;

namespace Nest
{
	public interface IDeleteWatchResponse : IResponse
	{
		[JsonProperty("found")]
		bool Found { get; }

		[JsonProperty("_id")]
		string Id { get; }

		[JsonProperty("_version")]
		int Version { get; }
	}

	public class DeleteWatchResponse : ResponseBase, IDeleteWatchResponse
	{
		public bool Found { get; internal set; }
		public string Id { get; internal set; }
		public int Version { get; internal set; }
	}
}
