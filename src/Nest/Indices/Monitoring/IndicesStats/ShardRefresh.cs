using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardRefresh
	{
		[JsonProperty("total")]
		public long Total { get; set; }
		[JsonProperty("total_time_in_millis")]
		public long TotalTimeInMilliseconds { get; set; }
		[JsonProperty("listeners")]
		public long Listeners { get; set; }
	}
}