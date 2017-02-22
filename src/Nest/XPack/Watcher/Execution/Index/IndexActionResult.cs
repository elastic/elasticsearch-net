using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	public class IndexActionResult
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("response")]
		public IndexActionResultIndexResponse Response { get; set; }
	}
}
