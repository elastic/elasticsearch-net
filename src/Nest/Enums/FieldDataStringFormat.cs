using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum FieldDataStringFormat
	{
		[EnumMember(Value = "paged_bytes")]
		PagedBytes,
		[EnumMember(Value = "fst")]
		Fst,
		[EnumMember(Value = "doc_values")]
		DocValues,
		[EnumMember(Value = "disabled")]
		Disabled
	}
}
