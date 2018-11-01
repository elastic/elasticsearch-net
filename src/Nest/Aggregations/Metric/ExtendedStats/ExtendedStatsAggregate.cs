namespace Nest
{
	public class ExtendedStatsAggregate : MetricAggregateBase
	{
		public double? Average { get; set; }
		public long Count { get; set; }
		public double? Max { get; set; }
		public double? Min { get; set; }
		public double? StdDeviation { get; set; }
		public StandardDeviationBounds StdDeviationBounds { get; set; }
		public double? Sum { get; set; }
		public double? SumOfSquares { get; set; }
		public double? Variance { get; set; }
	}

	public class StandardDeviationBounds
	{
		public double? Lower { get; set; }
		public double? Upper { get; set; }
	}
}
