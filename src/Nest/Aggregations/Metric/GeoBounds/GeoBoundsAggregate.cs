using Newtonsoft.Json;

namespace Nest
{
	public class GeoBoundsAggregate : MetricAggregateBase
	{
		[JsonProperty("bounds")]
		public GeoBounds Bounds { get; set; }
	}

	public class GeoBounds
	{
		[JsonProperty("top_left")]
		public LatLon TopLeft { get; set; }

		[JsonProperty("bottom_right")]
		public LatLon BottomRight { get; set; }
	}
}
