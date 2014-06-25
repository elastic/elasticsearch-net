using System.Runtime.Serialization;

namespace Nest
{
	public enum IndexOptions
	{
		[EnumMember(Value="docs")]
		Docs,
		[EnumMember(Value = "freqs")]
		Freqs,
		[EnumMember(Value = "positions")]
		Positions
	}
}
