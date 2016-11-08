using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class HipChatActionMessageResult
	{
		[JsonProperty("status")]
		public Status Status { get; set; }

		[JsonProperty("reason")]
		public string Reason { get; set; }

		[JsonProperty("request")]
		public HttpInputRequestResult Request { get; set; }

		[JsonProperty("response")]
		public HttpInputResponseResult Response { get; set; }

		[JsonProperty("room")]
		public string Room { get; set; }

		[JsonProperty("user")]
		public string User { get; set; }

		[JsonProperty("message")]
		public IHipChatMessage Message { get; set; }
	}
}
