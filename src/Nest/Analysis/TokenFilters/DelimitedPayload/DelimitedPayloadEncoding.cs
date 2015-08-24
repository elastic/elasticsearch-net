using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum DelimitedPayloadEncoding
	{
		[EnumMember(Value = "int")]
		Integer,
		[EnumMember(Value = "float")]
		Float,
		[EnumMember(Value = "identity")]
		Identity,
	}
}