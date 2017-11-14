using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardSegmentRouting
	{
		[JsonProperty("state")]
		public string State { get; internal set; }

		[JsonProperty("primary")]
		public bool Primary { get; internal set; }

		[JsonProperty("node")]
		public string Node { get; internal set; }
	}
}
