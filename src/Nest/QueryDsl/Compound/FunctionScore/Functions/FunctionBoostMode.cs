using System.Runtime.Serialization;


namespace Nest
{
	[StringEnum]
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
