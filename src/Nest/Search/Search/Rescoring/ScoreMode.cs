using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{

	public enum ScoreMode
	{
		[EnumMember(Value = "avg")]
		Average,

		[EnumMember(Value = "max")]
		Max,

		[EnumMember(Value = "min")]
		Min,

		[EnumMember(Value = "multiply")]
		Multiply,

		[EnumMember(Value = "total")]
		Total,
	}
}
