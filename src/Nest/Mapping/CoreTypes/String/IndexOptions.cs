using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum IndexOptions
	{
		[EnumMember(Value="docs")]
		Docs,
		[EnumMember(Value = "freqs")]
		Freqs,
		[EnumMember(Value = "positions")]
		Positions,
        [EnumMember(Value = "offsets")]
        Offsets,
	}
}
