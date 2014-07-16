using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum FieldValueFactorModifier
	{
		[EnumMember(Value = "none")]
		None,
		[EnumMember(Value = "count")]
		Log,
		[EnumMember(Value = "log1p")]
		Log1P,
		[EnumMember(Value = "log2p")]
		Log2P,
		[EnumMember(Value = "ln")]
		Ln,
		[EnumMember(Value = "ln1p")]
		Ln1P,
		[EnumMember(Value = "ln2p")]
		Ln2P,
		[EnumMember(Value = "square")]
		Square,
		[EnumMember(Value = "sqrt")]
		SquareRoot,
		[EnumMember(Value = "reciprocal")]
		Reciprocal
	}
}