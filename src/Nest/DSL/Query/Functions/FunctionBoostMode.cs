using System.Runtime.Serialization;

namespace Nest
{
	public enum FunctionBoostMode
	{
		[EnumMember(Value = "multiply")]
		Multiply,
		[EnumMember(Value = "replace")]
		Replace,
		[EnumMember(Value = "sum")]
		Sum,
		[EnumMember(Value = "avg")]
		Average,
		[EnumMember(Value = "max")]
		Max,
		[EnumMember(Value = "min")]
		Min
	}
}