using Newtonsoft.Json;

namespace Nest
{
	public class ExtendedStatsAggregate : StatsAggregate
	{
		[JsonProperty("sum_of_squares")]
		public double? SumOfSquares { get; internal set; }

		[JsonProperty("variance")]
		public double? Variance { get; internal set; }

		[JsonProperty("std_deviation")]
		public double? StdDeviation { get; internal set; }

		[JsonProperty("std_deviation_bounds")]
		public StandardDeviationBounds StdDeviationBounds { get; internal set; }
	}

	public class StandardDeviationBounds
	{
		[JsonProperty("upper")]
		public double? Upper { get; internal set; }

		[JsonProperty("lower")]
		public double? Lower { get; internal set; }
	}
}
