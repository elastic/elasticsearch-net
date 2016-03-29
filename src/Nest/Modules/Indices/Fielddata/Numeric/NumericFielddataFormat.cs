using System.Runtime.Serialization;

namespace Nest
{
	public enum NumericFielddataFormat
	{
		[EnumMember(Value = "array")]
		Array,
		[EnumMember(Value = "doc_values")]
		DocValues,
		[EnumMember(Value = "disabled")]
		Disabled
	}
}
