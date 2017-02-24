using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	public class RecoveryStats
	{
		[JsonProperty("current_as_source")]
		public long CurrentAsSource { get; set; }

		[JsonProperty("current_as_target")]
		public long CurrentAsTarget { get; set; }

		[JsonProperty("throttle_time")]
		public string ThrottleTime { get; set; }

		[JsonProperty("throttle_time_in_millis")]
		public long ThrottleTimeInMilliseconds { get; set; }

	}
}
