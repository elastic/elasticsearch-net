using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{

	public enum RangeRelation
	{
		[EnumMember(Value = "within")] Within,
		[EnumMember(Value = "contains")] Contains,
		[EnumMember(Value = "intersects")] Intersects
	}
}
