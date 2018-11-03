using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class EmailActionResult
	{
		[JsonProperty("account")]
		public string Account { get; set; }

		[JsonProperty("message")]
		public EmailResult Message { get; set; }

		[JsonProperty("reason")]
		public string Reason { get; set; }
	}
}
