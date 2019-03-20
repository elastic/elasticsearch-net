using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ActionType
	{
		[EnumMember(Value = "email")]
		Email,

		[EnumMember(Value = "webhook")]
		Webhook,

		[EnumMember(Value = "index")]
		Index,

		[EnumMember(Value = "logging")]
		Logging,

		[EnumMember(Value = "slack")]
		Slack,

		[EnumMember(Value = "pagerduty")]
		PagerDuty,
	}
}
