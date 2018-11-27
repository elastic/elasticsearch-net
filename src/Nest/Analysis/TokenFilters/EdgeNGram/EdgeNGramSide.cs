using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

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
