using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class HipChatActionMessageResult
	{
		[JsonProperty("message")]
		public IHipChatMessage Message { get; set; }

		[JsonProperty("reason")]
		public string Reason { get; set; }

		[JsonProperty("request")]
		public HttpInputRequestResult Request { get; set; }

		[JsonProperty("response")]
		public HttpInputResponseResult Response { get; set; }

		[JsonProperty("room")]
		public string Room { get; set; }

		[JsonProperty("status")]
		public Status Status { get; set; }

		[JsonProperty("user")]
		public string User { get; set; }
	}
}
