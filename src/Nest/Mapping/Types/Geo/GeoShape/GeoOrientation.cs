using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
	public enum GeoOrientation
	{
		[EnumMember(Value = "cw")]
		ClockWise,

		[EnumMember(Value = "ccw")]
		CounterClockWise
	}
}
