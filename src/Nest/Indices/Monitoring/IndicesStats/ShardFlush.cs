using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardFlush
	{
		[JsonProperty("total")]
		public long Total { get; set; }
		[JsonProperty("total_time_in_millis")]
		public long TotalTimeInMilliseconds { get; set; }
	}
}