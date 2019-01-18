using System.Runtime.Serialization;


namespace Nest
{
	[StringEnum]
	public enum MultiValueMode
	{
		[EnumMember(Value = "min")]
		Min,

		[EnumMember(Value = "max")]
		Max,

		[EnumMember(Value = "avg")]
		Average,

		[EnumMember(Value = "sum")]
		Sum
	}
}
