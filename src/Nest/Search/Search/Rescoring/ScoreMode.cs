using System.Runtime.Serialization;


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
