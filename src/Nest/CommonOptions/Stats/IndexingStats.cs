using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class IndexingStats
	{
		[JsonProperty(PropertyName = "index_total")]
		public long Total { get; set; }
		[JsonProperty(PropertyName = "index_time")]
		public string Time { get; set; }
		[JsonProperty(PropertyName = "index_time_in_millis")]
		public double TimeInMilliseconds { get; set; }
		[JsonProperty(PropertyName = "index_current")]
		public long Current { get; set; }
		[JsonProperty(PropertyName = "delete_total")]
		public long DeleteTotal { get; set; }
		[JsonProperty(PropertyName = "delete_time")]
		public string DeleteTime { get; set; }
		[JsonProperty(PropertyName = "delete_time_in_millis")]
		public double DeleteTimeInMilliseconds { get; set; }

		[JsonProperty(PropertyName = "delete_current")]
		public long DeleteCurrent { get; set; }
		
		[JsonProperty(PropertyName = "noop_update_total")]
		public long NoopUpdateTotal { get; set; }
		
		[JsonProperty(PropertyName = "is_throttled")]
		public bool IsThrottled { get; set; }
		
		[JsonProperty(PropertyName = "throttle_time_in_millis")]
		public long ThrottleTimeInMilliseconds { get; set; }
		
		[JsonProperty(PropertyName = "throttle_time")]
		public string ThrottleTime { get; set; }
		
		[JsonProperty(PropertyName = "types")]
		[JsonConverter(typeof(DictionaryKeysAreNotFieldNamesJsonConverter))]
		public Dictionary<string, TypeStats> Types { get; set; }
	}
}
