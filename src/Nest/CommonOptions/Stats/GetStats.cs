using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class GetStats
	{
		[JsonProperty("current")]
		public long Current { get; set; }

		[JsonProperty("exists_time")]
		public string ExistsTime { get; set; }
		[JsonProperty("exists_time_in_millis")]
		public long ExistsTimeInMilliseconds { get; set; }

		[JsonProperty("exists_total")]
		public long ExistsTotal { get; set; }

		[JsonProperty("missing_time")]
		public string MissingTime { get; set; }
		[JsonProperty("missing_time_in_millis")]
		public long MissingTimeInMilliseconds { get; set; }

		[JsonProperty("missing_total")]
		public long MissingTotal { get; set; }

		[JsonProperty("time")]
		public string Time { get; set; }
		[JsonProperty("time_in_millis")]
		public long TimeInMilliseconds { get; set; }

		[JsonProperty("total")]
		public long Total { get; set; }

	}
}
