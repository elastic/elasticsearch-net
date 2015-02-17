using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum GeoPrecisionUnit
	{
		[EnumMember(Value = "in")]
		Inch,
		[EnumMember(Value = "yd")]
		Yard,
		[EnumMember(Value = "mi")]
		Miles,
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
