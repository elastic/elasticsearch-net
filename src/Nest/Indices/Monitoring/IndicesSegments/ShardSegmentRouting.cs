using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardSegmentRouting
	{
		[JsonProperty("node")]
		public string Node { get; internal set; }

		[JsonProperty("primary")]
		public bool Primary { get; internal set; }

		[JsonProperty("state")]
		public string State { get; internal set; }
	}
}
