using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{

	public enum IndexOptions
	{
		[EnumMember(Value = "docs")]
		Docs,

		[EnumMember(Value = "freqs")]
		Freqs,

		[EnumMember(Value = "positions")]
		Positions,

		[EnumMember(Value = "offsets")]
		Offsets,
	}
}
