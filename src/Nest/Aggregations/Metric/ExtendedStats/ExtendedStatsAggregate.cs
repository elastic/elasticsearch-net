using Newtonsoft.Json;

namespace Nest
{
	public class ExtendedStatsAggregate : StatsAggregate
	{
		[JsonProperty("sum_of_squares")]
		public double? SumOfSquares { get; set; }

		[JsonProperty("variance")]
		public double? Variance { get; set; }

		[JsonProperty("std_deviation")]
		public double? StdDeviation { get; set; }

		[JsonProperty("std_deviation_bounds")]
		public StandardDeviationBounds StdDeviationBounds { get; set; }
	}

	public class StandardDeviationBounds
	{
		[JsonProperty("upper")]
		public double? Upper { get; set; }
		
		[JsonProperty("lower")]
		public double? Lower { get; set; }
	}
}