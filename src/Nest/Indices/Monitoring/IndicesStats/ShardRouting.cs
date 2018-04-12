using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardRouting
	{
		[JsonProperty("state")]
		public ShardRoutingState State { get; set; }

		[JsonProperty("primary")]
		public bool Primary { get; set; }

		[JsonProperty("node")]
		public string Node { get; set; }

		[JsonProperty("relocating_node")]
		public string RelocatingNode { get; set; }
	}
}