using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	public class PagerDutyActionResult
	{
		[JsonProperty("sent_event")]
		public PagerDutyActionEventResult SentEvent { get; set; }
	}
}