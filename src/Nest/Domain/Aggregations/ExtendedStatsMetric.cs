namespace Nest
{
	public class ExtendedStatsMetric : IMetricAggregation
	{
		public long Count { get; set; }
		public double? Min { get; set; }
		public double? Max { get; set; }
		public double? Average { get; set; }
		public double? Sum { get; set; }
		public double? SumOfSquares { get; set; }
		public double? Variance { get; set; }
		public double? StdDeviation { get; set; }
	}
}