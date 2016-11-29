using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class SlackActionMessageResult
	{
		[JsonProperty("status")]
		public Status Status { get; set; }

		[JsonProperty("reason")]
		public string Reason { get; set; }

		[JsonProperty("request")]
		public HttpInputRequestResult Request { get; set; }

		[JsonProperty("response")]
		public HttpInputResponseResult Response { get; set; }

		[JsonProperty("to")]
		public string To { get; set; }

		[JsonProperty("message")]
		public ISlackMessage Message { get; set; }
	}
}
