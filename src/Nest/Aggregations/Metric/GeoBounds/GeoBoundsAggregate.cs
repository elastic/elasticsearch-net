namespace Nest
{
	public class GeoBoundsAggregate : MetricAggregateBase
	{
		public GeoBoundsAggregate() => Bounds = new GeoBounds();

		public GeoBounds Bounds { get; set; }
	}

	public class GeoBounds
	{
		public LatLon BottomRight { get; set; }
		public LatLon TopLeft { get; set; }
	}
}
