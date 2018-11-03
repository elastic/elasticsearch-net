using Newtonsoft.Json;

namespace Nest
{
	public class GeoCentroidAggregate : MetricAggregateBase
	{
		[JsonProperty("count")]
		public long Count { get; set; }

		[JsonProperty("location")]
		public GeoLocation Location { get; set; }
	}
}
