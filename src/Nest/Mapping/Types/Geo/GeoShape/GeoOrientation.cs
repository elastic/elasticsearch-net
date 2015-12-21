using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum GeoOrientation
	{
		[EnumMember(Value = "cw")]
		ClockWise,
		[EnumMember(Value = "ccw")]
		CounterClockWise
	}
}
