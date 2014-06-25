using System.Runtime.Serialization;

namespace Nest
{
	public enum Operator
	{
		[EnumMember(Value = "and")]
		And,
		[EnumMember(Value = "or")]
		Or
	}
}
