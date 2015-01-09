using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum GeoShapeRelation
	{
		[EnumMember(Value = "intersects")]
		Intersects,
		[EnumMember(Value = "disjoint")]
		Disjoint,
		[EnumMember(Value = "within")]
		Within
	}
}
