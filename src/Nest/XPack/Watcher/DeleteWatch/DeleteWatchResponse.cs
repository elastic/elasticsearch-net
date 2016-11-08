using Newtonsoft.Json;

namespace Nest
{
	public interface IDeleteWatchResponse : IResponse
	{
		[JsonProperty("_id")]
		string Id { get; }

		[JsonProperty("_version")]
		int Version { get; }

		[JsonProperty("found")]
		bool Found { get; }
	}

	public class DeleteWatchResponse : ResponseBase, IDeleteWatchResponse
	{
		public string Id { get; internal set; }
		public int Version { get; internal set; }
		public bool Found { get; internal set; }
	}
}
