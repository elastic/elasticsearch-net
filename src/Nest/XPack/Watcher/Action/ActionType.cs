using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

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

		[EnumMember(Value = "hipchat")]
		HipChat,

		[EnumMember(Value = "slack")]
		Slack,

		[EnumMember(Value = "pagerduty")]
		PagerDuty,
	}
}
