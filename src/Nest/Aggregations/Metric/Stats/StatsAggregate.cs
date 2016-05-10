using Newtonsoft.Json;

namespace Nest
{
	public class StatsAggregate : MetricAggregateBase
	{
		[JsonProperty("count")]
		public long Count { get; internal set; }

		[JsonProperty("min")]
		public double? Min { get; internal set; }

		[JsonProperty("max")]
		public double? Max { get; internal set; }

		[JsonProperty("avg")]
		public double? Average { get; internal set; }

		[JsonProperty("sum")]
		public double? Sum { get; internal set; }
	}
}
