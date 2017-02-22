using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Nest_5_2_0
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum Result
	{
		[EnumMember(Value = "created")]
		Created,
		[EnumMember(Value = "updated")]
		Updated,
		[EnumMember(Value = "deleted")]
		Deleted,
		[EnumMember(Value = "not_found")]
		NotFound,
		[EnumMember(Value = "noop")]
		Noop
	}
}
