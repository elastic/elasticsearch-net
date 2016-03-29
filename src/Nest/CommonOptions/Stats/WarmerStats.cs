using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class WarmerStats
	{
		[JsonProperty("current")]
		public long Current { get; set; }

		[JsonProperty("total")]
		public long Total { get; set; }

		[JsonProperty("total_time")]
		public string TotalTime { get; set; }

		[JsonProperty("total_time_in_millis")]
		public long TotalTimeInMilliseconds { get; set; }
	}
}