using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum EdgeNGramSide
	{
		[EnumMember(Value = "front")]
		Front,
		[EnumMember(Value = "back")]
		Back,
	}
}