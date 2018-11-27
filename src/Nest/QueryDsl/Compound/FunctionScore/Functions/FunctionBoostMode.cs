using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

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
