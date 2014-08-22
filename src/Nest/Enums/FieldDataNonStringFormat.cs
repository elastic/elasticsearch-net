using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum FieldDataNonStringFormat
	{
		[EnumMember(Value = "array")]
		Array,
		[EnumMember(Value = "doc_values")]
		DocValues,
		[EnumMember(Value = "disabled")]
		Disabled
	}
}
