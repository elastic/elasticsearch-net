using System.Runtime.Serialization;

namespace Nest
{
	public enum DistanceUnit
	{
		[EnumMember(Value = "in")]
		Inch,

		[EnumMember(Value = "ft")]
		Feet,

		[EnumMember(Value = "yd")]
		Yards,

		[EnumMember(Value = "mi")]
		Miles,

		[EnumMember(Value = "nmi")]
		[AlternativeEnumMember("NM")]
		NauticalMiles,

		[EnumMember(Value = "km")]
		Kilometers,

		[EnumMember(Value = "m")]
		Meters,

		[EnumMember(Value = "cm")]
		Centimeters,

		[EnumMember(Value = "mm")]
		Millimeters
	}
}
