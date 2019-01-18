using System.Runtime.Serialization;

namespace Nest
{
	[StringEnum]
	public enum EdgeNGramSide
	{
		[EnumMember(Value = "front")]
		Front,

		[EnumMember(Value = "back")]
		Back,
	}
}
