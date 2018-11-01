namespace Nest
{
	public class StatsAggregate : MetricAggregateBase
	{
		public double? Average { get; set; }
		public long Count { get; set; }
		public double? Max { get; set; }
		public double? Min { get; set; }
		public double? Sum { get; set; }
	}
}
