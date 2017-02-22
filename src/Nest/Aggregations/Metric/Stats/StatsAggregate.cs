namespace Nest_5_2_0
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