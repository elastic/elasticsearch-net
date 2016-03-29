using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class SearchStats
	{
		[JsonProperty(PropertyName = "fetch_current")]
		public long FetchCurrent { get; set; }

		[JsonProperty(PropertyName = "fetch_time")]
		public string FetchTime { get; set; }
		[JsonProperty(PropertyName = "fetch_time_in_millis")]
		public long FetchTimeInMilliseconds { get; set; }

		[JsonProperty(PropertyName = "fetch_total")]
		public long FetchTotal { get; set; }

		[JsonProperty(PropertyName = "query_current")]
		public long QueryCurrent { get; set; }

		[JsonProperty(PropertyName = "query_time")]
		public string QueryTime { get; set; }
		[JsonProperty(PropertyName = "query_time_in_millis")]
		public long QueryTimeInMilliseconds { get; set; }

		[JsonProperty(PropertyName = "query_total")]
		public long QueryTotal { get; set; }

		[JsonProperty(PropertyName = "scroll_current")]
		public long ScrollCurrent { get; set; }

		[JsonProperty(PropertyName = "scroll_time")]
		public string ScrollTime { get; set; }
		[JsonProperty(PropertyName = "scroll_time_in_millis")]
		public long ScrollTimeInMilliseconds { get; set; }

		[JsonProperty(PropertyName = "scroll_total")]
		public long ScrollTotal { get; set; }
	}
}
