using Newtonsoft.Json;

namespace Nest
{
	public class RecoveryTranslogStatus
	{
		[JsonProperty("recovered")]
		public long Recovered { get; internal set; }

		[JsonProperty("total_time_in_millis")]
		public string TotalTimeInMilliseconds { get; internal set; }
	}
}