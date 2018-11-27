using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{

	public enum TermVectorOption
	{
		[EnumMember(Value = "no")]
		No,

		[EnumMember(Value = "yes")]
		Yes,

		[EnumMember(Value = "with_offsets")]
		WithOffsets,

		[EnumMember(Value = "with_positions")]
		WithPositions,

		[EnumMember(Value = "with_positions_offsets")]
		WithPositionsOffsets,

		[EnumMember(Value = "with_positions_offsets_payloads")]
		WithPositionsOffsetsPayloads
	}
}
