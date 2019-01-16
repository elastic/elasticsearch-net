using System.Runtime.Serialization;


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
