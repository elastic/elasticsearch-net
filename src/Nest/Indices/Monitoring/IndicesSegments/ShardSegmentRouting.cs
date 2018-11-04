using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardSegmentRouting
	{
		[JsonProperty(PropertyName = "node")]
		public string Node { get; internal set; }

		[JsonProperty(PropertyName = "primary")]
		public bool Primary { get; internal set; }

		[JsonProperty(PropertyName = "state")]
		public string State { get; internal set; }
	}
}
