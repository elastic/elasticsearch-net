using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum Status
	{
		[EnumMember(Value = "success")]
		Success,

		[EnumMember(Value = "failure")]
		Failure,

		[EnumMember(Value = "simulated")]
		Simulated,

		[EnumMember(Value = "throttled")]
		Throttled
	}
}