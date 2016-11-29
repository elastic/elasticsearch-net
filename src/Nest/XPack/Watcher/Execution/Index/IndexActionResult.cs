using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class IndexActionResult
	{
		[JsonProperty("id")]
		public Id Id { get; set; }

		[JsonProperty("response")]
		public IndexActionResultIndexResponse Response { get; set; }
	}
}
