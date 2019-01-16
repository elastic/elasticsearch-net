using System.Runtime.Serialization;


namespace Nest
{
	[StringEnum]
	public enum Operator
	{
		[EnumMember(Value = "and")]
		And,

		[EnumMember(Value = "or")]
		Or
	}
}
