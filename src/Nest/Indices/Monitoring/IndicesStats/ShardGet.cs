using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardGet
	{
		[JsonProperty("total")]
		public long Total { get; set; }

		[JsonProperty("time_in_millis")]
		public long TimeInMilliseconds { get; set; }

		[JsonProperty("exists_total")]
		public long ExistsTotal { get; set; }

		[JsonProperty("exists_time_in_millis")]
		public long ExistsTimeInMilliseconds { get; set; }

		[JsonProperty("missing_total")]
		public long MissingTotal { get; set; }

		[JsonProperty("missing_time_in_millis")]
		public long MissingTimeInMilliseconds { get; set; }

		[JsonProperty("current")]
		public long Current { get; set; }
	}
}