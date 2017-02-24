using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	public class StoreStats
	{
		[JsonProperty(PropertyName = "size")]
		public string Size { get; set; }

		[JsonProperty(PropertyName = "size_in_bytes")]
		public double SizeInBytes { get; set; }

		[JsonProperty("throttle_time")]
		public string ThrottleTime { get; set; }

		[JsonProperty("throttle_time_in_millis")]
		public long ThrottleTimeInMilliseconds { get; set; }
	}

}
