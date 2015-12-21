using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
    public enum ScoreMode
    {
		[EnumMember(Value = "avg")]
        Average,
		[EnumMember(Value = "first")]
        First,
		[EnumMember(Value = "max")]
        Max,
		[EnumMember(Value = "min")]
        Min,
		[EnumMember(Value = "multiply")]
        Multiply,
		[EnumMember(Value = "total")]
        Total,
		[EnumMember(Value = "sum")]
		Sum
    }
}
