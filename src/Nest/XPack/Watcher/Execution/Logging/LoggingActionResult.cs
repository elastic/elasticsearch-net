using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class LoggingActionResult
	{
		[JsonProperty("logged_text")]
		public string LoggedText { get; set; }
	}
}