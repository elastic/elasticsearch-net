using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardStatsStore
	{
		[JsonProperty("size_in_bytes")]
		public long SizeInBytes { get; internal set; }

		[JsonProperty("throttle_time_in_millis")]
		public long ThrottleTimeInMilliseconds { get; internal set; }
	}
}
