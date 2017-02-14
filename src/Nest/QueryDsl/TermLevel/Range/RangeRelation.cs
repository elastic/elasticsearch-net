using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum RangeRelation
	{
		[EnumMember(Value="within")] Within,
		[EnumMember(Value="contains")] Contains,
		[EnumMember(Value="intersects")] Intersects
	}
}
