using System.Runtime.Serialization;
using System.Runtime.Serialization;


namespace Nest
{

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
