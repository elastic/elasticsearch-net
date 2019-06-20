namespace Nest
{
	public class ExtendedStatsAggregate : StatsAggregate
	{
		public double? StdDeviation { get; set; }
		public StandardDeviationBounds StdDeviationBounds { get; set; }
		public double? SumOfSquares { get; set; }
		public double? Variance { get; set; }
	}

	public class StandardDeviationBounds
	{
		public double? Lower { get; set; }
		public double? Upper { get; set; }
	}
}
