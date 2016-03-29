using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ChildScoreMode
	{
		[EnumMember(Value = "none")]
		None,
		[EnumMember(Value = "avg")]
		Average,
		[EnumMember(Value = "sum")]
		Sum,
		[EnumMember(Value = "max")]
		Max,
		[EnumMember(Value = "min")]
		Min
	}
}
