using System.Runtime.Serialization;

namespace Nest
{
	public class GeoCentroidAggregate : MetricAggregateBase
	{
		[DataMember(Name ="count")]
		public long Count { get; set; }

		[DataMember(Name ="location")]
		public GeoLocation Location { get; set; }
	}
}
