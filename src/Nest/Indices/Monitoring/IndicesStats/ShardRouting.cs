using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardRouting
	{
		[JsonProperty("state")]
		public ShardRoutingState State { get; internal set; }

		[JsonProperty("primary")]
		public bool Primary { get; internal set; }

		[JsonProperty("node")]
		public string Node { get; internal set; }

		[JsonProperty("relocating_node")]
		public string RelocatingNode { get; internal set; }
	}
}
