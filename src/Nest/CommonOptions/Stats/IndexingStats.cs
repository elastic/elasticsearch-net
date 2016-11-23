using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class IndexingStats
	{
		[JsonProperty("delete_current")]
		public long DeleteCurrent { get; set; }

		[JsonProperty("delete_time")]
		public string DeleteTime { get; set; }

		[JsonProperty("delete_time_in_millis")]
		public long DeleteTimeInMilliseconds { get; set; }

		[JsonProperty("delete_total")]
		public long DeleteTotal { get; set; }

		[JsonProperty("index_current")]
		public long Current { get; set; }

		[JsonProperty("index_time")]
		public string Time { get; set; }

		[JsonProperty("index_time_in_millis")]
		public long TimeInMilliseconds { get; set; }

		[JsonProperty("index_total")]
		public long Total { get; set; }

		[JsonProperty("is_throttled")]
		public bool IsThrottled { get; set; }

		[JsonProperty("noop_update_total")]
		public long NoopUpdateTotal { get; set; }

		[JsonProperty("throttle_time")]
		public string ThrottleTime { get; set; }

		[JsonProperty("throttle_time_in_millis")]
		public long ThrottleTimeInMilliseconds { get; set; }

		[JsonProperty("types")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, IndexingStats>))]
		public IReadOnlyDictionary<string, IndexingStats> Types { get; set; }
	}
}
