using Newtonsoft.Json;

namespace Nest
{
	public class StatsAggregate : MetricAggregateBase
	{
		[JsonProperty("count")]
		public long Count { get; set; }

		[JsonProperty("min")]
		public double? Min { get; set; }

		[JsonProperty("max")]
		public double? Max { get; set; }

		[JsonProperty("avg")]
		public double? Average { get; set; }

		[JsonProperty("sum")]
		public double? Sum { get; set; }
	}
}