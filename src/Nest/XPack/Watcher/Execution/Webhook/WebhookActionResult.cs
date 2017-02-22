using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public class WebhookActionResult
	{
		[JsonProperty("request")]
		public HttpInputRequestResult Request { get; set; }

		[JsonProperty("response")]
		public HttpInputResponseResult Response { get; set; }
	}
}
