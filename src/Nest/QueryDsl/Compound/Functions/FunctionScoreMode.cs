using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum FunctionScoreMode
	{
		[EnumMember(Value = "multiply")]
		Multiply,
		[EnumMember(Value = "sum")]
		Sum,
		[EnumMember(Value = "avg")]
		Average,
		[EnumMember(Value = "first")]
		First,
		[EnumMember(Value = "max")]
		Max,
		[EnumMember(Value = "min")]
		Min
	}
}