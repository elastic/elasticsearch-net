using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardSearch
	{
		[JsonProperty("open_contexts")]
		public long OpenContexts { get; internal set; }
		[JsonProperty("query_total")]
		public long QueryTotal { get; internal set; }
		[JsonProperty("query_time_in_millis")]
		public long QueryTimeInMilliseconds { get; internal set; }
		[JsonProperty("query_current")]
		public long QueryCurrent { get; internal set; }
		[JsonProperty("fetch_total")]
		public long FetchTotal { get; internal set; }
		[JsonProperty("fetch_time_in_millis")]
		public long FetchTimeInMilliseconds { get; internal set; }
		[JsonProperty("fetch_current")]
		public long FetchCurrent { get; internal set; }
		[JsonProperty("scroll_total")]
		public long ScrollTotal { get; internal set; }
		[JsonProperty("scroll_time_in_millis")]
		public long ScrollTimeInMilliseconds { get; internal set; }
		[JsonProperty("scroll_current")]
		public long ScrollCurrent { get; internal set; }
		[JsonProperty("suggest_total")]
		public long SuggestTotal { get; internal set; }
		[JsonProperty("suggest_time_in_millis")]
		public long SuggestTimeInMilliseconds { get; internal set; }
		[JsonProperty("suggest_current")]
		public long SuggestCurrent { get; internal set; }
	}
}
