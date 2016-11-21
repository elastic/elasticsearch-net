using Newtonsoft.Json;

namespace Nest
{
	public class PagerDutyActionEventResult
	{
		[JsonProperty("event")]
		public PagerDutyEvent Event { get; set; }

		[JsonProperty("reason")]
		public string Reason { get; set; }

		[JsonProperty("request")]
		public HttpInputRequestResult Request { get; set; }

		[JsonProperty("response")]
		public HttpInputResponseResult Response { get; set; }
	}
}
