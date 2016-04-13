using Newtonsoft.Json;

namespace Nest
{
	public class GraphVertex
	{
		[JsonProperty("depth")]
		public long Depth { get; internal set; }

		[JsonProperty("field")]
		public string Field { get; internal set; }

		[JsonProperty("term")]
		public string Term { get; internal set; }

		[JsonProperty("weight")]
		public double Weight { get; internal set; }

	}
}
