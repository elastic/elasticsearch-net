using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum NumberType
	{
		[EnumMember(Value = "float")]
		Float,
		[EnumMember(Value = "half_float")]
		HalfFloat,
		[EnumMember(Value = "scaled_float")]
		ScaledFloat,
		[EnumMember(Value = "double")]
		Double,
		[EnumMember(Value = "integer")]
		Integer,
		[EnumMember(Value = "long")]
		Long,
		[EnumMember(Value = "short")]
		Short,
		[EnumMember(Value = "byte")]
		Byte
	}
}
