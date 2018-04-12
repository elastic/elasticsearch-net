using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardMerges
	{
		[JsonProperty("current")]
		public long Current { get; internal set; }
		[JsonProperty("current_docs")]
		public long CurrentDocuments { get; internal set; }
		[JsonProperty("current_size_in_bytes")]
		public long CurrentSizeInBytes { get; internal set; }
		[JsonProperty("total")]
		public long Total { get; internal set; }
		[JsonProperty("total_time_in_millis")]
		public long TotalTimeInMilliseconds { get; internal set; }
		[JsonProperty("total_docs")]
		public long TotalDocuments { get; internal set; }
		[JsonProperty("total_size_in_bytes")]
		public long TotalSizeInBytes { get; internal set; }
		[JsonProperty("total_stopped_time_in_millis")]
		public long TotalStoppedTimeInMilliseconds { get; internal set; }
		[JsonProperty("total_throttled_time_in_millis")]
		public long TotalThrottledTimeInMilliseconds { get; internal set; }
		[JsonProperty("total_auto_throttle_in_bytes")]
		public long TotalAutoThrottleInBytes { get; internal set; }
	}
}
