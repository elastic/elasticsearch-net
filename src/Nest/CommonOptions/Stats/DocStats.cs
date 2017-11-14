using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class DocStats
	{
		[JsonProperty("count")]
		public long Count { get; set; }

		[JsonProperty("deleted")]
		public long Deleted { get; set; }
	}
}
