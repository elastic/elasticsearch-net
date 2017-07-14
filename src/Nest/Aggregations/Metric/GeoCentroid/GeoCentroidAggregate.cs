namespace Nest
{
	public class GeoCentroidAggregate : MetricAggregateBase
	{
		public GeoLocation Location { get; set; }

		//TODO non nullable in 6.0, introduced in 5.5
		public long? Count { get; set; }
	}
}
