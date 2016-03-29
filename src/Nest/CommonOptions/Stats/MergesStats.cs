using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class MergesStats
	{
	
		[JsonProperty(PropertyName = "current")]
		public long Current { get; set; }

		[JsonProperty(PropertyName = "current_docs")]
		public long CurrentDocuments { get; set; }

		[JsonProperty(PropertyName = "current_size")]
		public string CurrentSize { get; set; }
		[JsonProperty(PropertyName = "current_size_in_bytes")]
		public long CurrentSizeInBytes { get; set; }

		[JsonProperty(PropertyName = "total")]
		public long Total { get; set; }

		[JsonProperty(PropertyName = "total_auto_throttle")]
		public string TotalAutoThrottle { get; set; }
		[JsonProperty(PropertyName = "total_auto_throttle_in_bytes")]
		public long TotalAutoThrottleInBytes { get; set; }

		[JsonProperty(PropertyName = "total_docs")]
		public long TotalDocuments { get; set; }

		[JsonProperty(PropertyName = "total_size")]
		public string TotalSize { get; set; }
		[JsonProperty(PropertyName = "total_size_in_bytes")]
		public string TotalSizeInBytes { get; set; }

		[JsonProperty(PropertyName = "total_stopped_time")]
		public string TotalStoppedTime { get; set; }
		[JsonProperty(PropertyName = "total__stopped_time_in_millis")]
		public long TotalStoppedTimeInMilliseconds { get; set; }

		[JsonProperty(PropertyName = "total_throttled_time")]
		public string TotalThrottledTime { get; set; }
		[JsonProperty(PropertyName = "total_throttled_time_in_millis")]
		public long TotalThrottledTimeInMilliseconds { get; set; }

		[JsonProperty(PropertyName = "total_time")]
		public string TotalTime { get; set; }
		[JsonProperty(PropertyName = "total_time_in_millis")]
		public long TotalTimeInMilliseconds { get; set; }
	}
}
