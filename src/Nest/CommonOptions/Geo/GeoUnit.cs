using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum GeoUnit
	{
		[EnumMember(Value = "km")]
		Kilometers,
		[EnumMember(Value = "mi")]
		Miles
	}
}
