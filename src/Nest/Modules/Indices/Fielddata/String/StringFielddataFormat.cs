using System;
using System.Runtime.Serialization;

namespace Nest
{
	public enum StringFielddataFormat
	{
		[EnumMember(Value = "paged_bytes")]
		PagedBytes,
		[Obsolete("Deprecated in 2.0, will be removed in next major version release")]
		[EnumMember(Value = "doc_values")]
		DocValues,
		[EnumMember(Value = "disabled")]
		Disabled
	}
}
