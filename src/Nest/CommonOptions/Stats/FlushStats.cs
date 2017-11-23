using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class FlushStats
	{

		[JsonProperty("total")]
		public long Total { get; set; }

		[JsonProperty("total_time")]
		public string TotalTime { get; set; }
		[JsonProperty("total_time_in_millis")]
		public long TotalTimeInMilliseconds { get; set; }

	}
}
