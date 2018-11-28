using System.Runtime.Serialization;
using System.Runtime.Serialization;


namespace Nest
{

	public enum EdgeNGramSide
	{
		[EnumMember(Value = "front")]
		Front,

		[EnumMember(Value = "back")]
		Back,
	}
}
