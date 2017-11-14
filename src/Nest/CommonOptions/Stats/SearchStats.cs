using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class SearchStats
	{
		[JsonProperty("open_contexts")]
		public long OpenContexts { get; set; }

		[JsonProperty("fetch_current")]
		public long FetchCurrent { get; set; }

		[JsonProperty("fetch_time_in_millis")]
		public long FetchTimeInMilliseconds { get; set; }

		[JsonProperty("fetch_total")]
		public long FetchTotal { get; set; }

		[JsonProperty("query_current")]
		public long QueryCurrent { get; set; }

		[JsonProperty("query_time_in_millis")]
		public long QueryTimeInMilliseconds { get; set; }

		[JsonProperty("query_total")]
		public long QueryTotal { get; set; }

		[JsonProperty("scroll_current")]
		public long ScrollCurrent { get; set; }

		[JsonProperty("scroll_time_in_millis")]
		public long ScrollTimeInMilliseconds { get; set; }

		[JsonProperty("scroll_total")]
		public long ScrollTotal { get; set; }

		[JsonProperty("suggest_current")]
		public long SuggestCurrent { get; set; }

		[JsonProperty("suggest_time_in_millis")]
		public long SuggestTimeInMilliseconds { get; set; }

		[JsonProperty("suggest_total")]
		public long SuggestTotal { get; set; }
	}
}
