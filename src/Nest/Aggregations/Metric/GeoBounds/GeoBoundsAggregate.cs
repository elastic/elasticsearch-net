using Newtonsoft.Json;

namespace Nest
{
	public class GeoBoundsAggregate : MetricAggregateBase
	{
		[JsonProperty("bounds")]
		public GeoBounds Bounds { get; internal set; }
	}

	public class GeoBounds
	{
		[JsonProperty("top_left")]
		public LatLon TopLeft { get; internal set; }

		[JsonProperty("bottom_right")]
		public LatLon BottomRight { get; internal set; }
	}
}
