using System.Runtime.Serialization;
using System.Runtime.Serialization;


namespace Nest
{

	public enum ConditionType
	{
		[EnumMember(Value = "always")]
		Always,

		[EnumMember(Value = "never")]
		Never,

		[EnumMember(Value = "script")]
		Script,

		[EnumMember(Value = "compare")]
		Compare,

		[EnumMember(Value = "array_compare")]
		ArrayCompare
	}
}
