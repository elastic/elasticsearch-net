using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{

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
