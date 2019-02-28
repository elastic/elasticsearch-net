using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[StringEnum]
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
