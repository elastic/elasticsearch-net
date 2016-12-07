using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
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
