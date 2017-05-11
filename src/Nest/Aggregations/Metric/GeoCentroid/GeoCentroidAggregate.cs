namespace Nest
{
	public class GeoCentroidAggregate : MetricAggregateBase
	{
		public GeoLocation Location { get; set; }
		public long Count { get; set; }
	}
}
