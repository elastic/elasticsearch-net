using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardMerges
	{
		[JsonProperty("current")]
		public long Current { get; set; }
		[JsonProperty("current_docs")]
		public long CurrentDocuments { get; set; }
		[JsonProperty("current_size_in_bytes")]
		public long CurrentSizeInBytes { get; set; }
		[JsonProperty("total")]
		public long Total { get; set; }
		[JsonProperty("total_time_in_millis")]
		public long TotalTimeInMilliseconds { get; set; }
		[JsonProperty("total_docs")]
		public long TotalDocuments { get; set; }
		[JsonProperty("total_size_in_bytes")]
		public long TotalSizeInBytes { get; set; }
		[JsonProperty("total_stopped_time_in_millis")]
		public long TotalStoppedTimeInMilliseconds { get; set; }
		[JsonProperty("total_throttled_time_in_millis")]
		public long TotalThrottledTimeInMilliseconds { get; set; }
		[JsonProperty("total_auto_throttle_in_bytes")]
		public long TotalAutoThrottleInBytes { get; set; }
	}
}