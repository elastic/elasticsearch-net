using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum GeoDistanceType
	{
		[EnumMember(Value = "sloppy_arc")]
		SloppyArc,
		[EnumMember(Value = "arc")]
		Arc,
		[EnumMember(Value = "plane")]
		Plane
	}
}