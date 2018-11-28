using System.Runtime.Serialization;
using System.Runtime.Serialization;


namespace Nest
{

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
