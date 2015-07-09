using Newtonsoft.Json;

namespace Nest
{
	public class RecoveryTranslogStatus
	{
		[JsonProperty("recovered")]
		public long Recovered { get; internal set; }

		[JsonProperty("total")]
		public long Total { get; internal set; }

		[JsonProperty("percent")]
		public string Percent { get; internal set; }

		[JsonProperty("total_on_start")]
		public long TotalOnStart { get; internal set; }

		[JsonProperty("total_time")]
		public string TotalTime { get; internal set; }

		//TODO should be a long in 2.0
		[JsonProperty("total_time_in_millis")]
		public string TotalTimeInMilliseconds { get; internal set; }
	}
}