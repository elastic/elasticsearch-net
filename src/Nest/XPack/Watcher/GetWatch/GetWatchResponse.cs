using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public interface IGetWatchResponse : IResponse
	{
		[JsonProperty("found")]
		bool Found { get; }

		[JsonProperty("_id")]
		Id Id { get; }

		[JsonProperty("_status")]
		WatchStatus Status { get; }

		[JsonProperty("watch")]
		Watch Watch { get; }
	}

	public class GetWatchResponse : ResponseBase, IGetWatchResponse
	{
		public bool Found { get; internal set; }
		public Id Id { get; internal set; }
		public WatchStatus Status { get; internal set; }
		public Watch Watch { get; internal set; }
	}
}
