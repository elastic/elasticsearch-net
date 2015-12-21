using System.Runtime.Serialization;

namespace Nest
{
	public enum StringFielddataFormat
	{
		[EnumMember(Value = "paged_bytes")]
		PagedBytes,
		[EnumMember(Value = "doc_values")]
		DocValues,
		[EnumMember(Value = "disabled")]
		Disabled
	}
}
