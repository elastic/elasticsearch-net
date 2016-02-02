namespace Nest
{
	public class StatsAggregate : MetricAggregateBase
	{
		public long Count { get; set; }
		public double? Min { get; set; }
		public double? Max { get; set; }
		public double? Average { get; set; }
		public double? Sum { get; set; }
	}
}