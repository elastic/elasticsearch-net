using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	public class LoggingActionResult
	{
		[JsonProperty("logged_text")]
		public string LoggedText { get; set; }
	}
}