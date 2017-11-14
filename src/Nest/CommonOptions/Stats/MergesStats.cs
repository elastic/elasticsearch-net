using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class MergesStats
	{

		[JsonProperty("current")]
		public long Current { get; set; }

		[JsonProperty("current_docs")]
		public long CurrentDocuments { get; set; }

		[JsonProperty("current_size")]
		public string CurrentSize { get; set; }
		[JsonProperty("current_size_in_bytes")]
		public long CurrentSizeInBytes { get; set; }

		[JsonProperty("total")]
		public long Total { get; set; }

		[JsonProperty("total_auto_throttle")]
		public string TotalAutoThrottle { get; set; }
		[JsonProperty("total_auto_throttle_in_bytes")]
		public long TotalAutoThrottleInBytes { get; set; }

		[JsonProperty("total_docs")]
		public long TotalDocuments { get; set; }

		[JsonProperty("total_size")]
		public string TotalSize { get; set; }
		[JsonProperty("total_size_in_bytes")]
		public string TotalSizeInBytes { get; set; }

		[JsonProperty("total_stopped_time")]
		public string TotalStoppedTime { get; set; }
		[JsonProperty("total__stopped_time_in_millis")]
		public long TotalStoppedTimeInMilliseconds { get; set; }

		[JsonProperty("total_throttled_time")]
		public string TotalThrottledTime { get; set; }
		[JsonProperty("total_throttled_time_in_millis")]
		public long TotalThrottledTimeInMilliseconds { get; set; }

		[JsonProperty("total_time")]
		public string TotalTime { get; set; }
		[JsonProperty("total_time_in_millis")]
		public long TotalTimeInMilliseconds { get; set; }
	}
}
