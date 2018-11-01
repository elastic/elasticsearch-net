using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum Result
	{
		Error,

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
