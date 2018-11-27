using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{

	public enum GeoOrientation
	{
		[EnumMember(Value = "cw")]
		ClockWise,

		[EnumMember(Value = "ccw")]
		CounterClockWise
	}
}
