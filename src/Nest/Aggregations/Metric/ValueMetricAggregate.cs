using Newtonsoft.Json;

namespace Nest
{
	public class ValueAggregate : MetricAggregateBase
	{
		[JsonProperty("value")]
		public double? Value { get; set; }
	}
}