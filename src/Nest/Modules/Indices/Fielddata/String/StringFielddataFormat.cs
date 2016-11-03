using System;
using System.Runtime.Serialization;

namespace Nest
{
	public enum StringFielddataFormat
	{
		[EnumMember(Value = "paged_bytes")]
		PagedBytes,
		[Obsolete("Deprecated in 2.0.0. Removed in 5.0.0")]
		[EnumMember(Value = "doc_values")]
		DocValues,
		[EnumMember(Value = "disabled")]
		Disabled
	}
}
