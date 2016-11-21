using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum EmailPriority
	{
		[EnumMember(Value = "lowest")]
		Lowest,
		[EnumMember(Value = "low")]
		Low,
		[EnumMember(Value = "normal")]
		Normal,
		[EnumMember(Value = "high")]
		High,
		[EnumMember(Value = "highest")]
		Highest
	}
}