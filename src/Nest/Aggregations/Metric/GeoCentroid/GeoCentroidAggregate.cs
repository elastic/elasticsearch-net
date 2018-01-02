using Newtonsoft.Json;

namespace Nest
{
	public class GeoCentroidAggregate : MetricAggregateBase
	{
		[JsonProperty("location")]
		public GeoLocation Location { get; set; }
		[JsonProperty("count")]
		public long Count { get; set; }
	}
}
