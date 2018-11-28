using System.Runtime.Serialization;
using System.Runtime.Serialization;


namespace Nest
{

	public enum NestedScoreMode
	{
		[EnumMember(Value = "avg")]
		Average,

		[EnumMember(Value = "sum")]
		Sum,

		[EnumMember(Value = "min")]
		Min,

		[EnumMember(Value = "max")]
		Max,

		[EnumMember(Value = "none")]
		None
	}
}
