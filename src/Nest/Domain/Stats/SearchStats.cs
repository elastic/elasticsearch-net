using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class SearchStats
	{
		[JsonProperty(PropertyName = "query_total")]
		public int QueryTotal { get; set; }
		[JsonProperty(PropertyName = "query_time")]
		public string QueryTime { get; set; }
		[JsonProperty(PropertyName = "query_time_in_millis")]
		public double QueryTimeInMilliseconds { get; set; }
		[JsonProperty(PropertyName = "query_current")]
		public int QueryCurrent { get; set; }

		[JsonProperty(PropertyName = "fetch_total")]
		public int FetchTotal { get; set; }
		[JsonProperty(PropertyName = "fetch_time")]
		public string FetchTime { get; set; }
		[JsonProperty(PropertyName = "fetch_time_in_millis")]
		public double FetchTimeInMilliseconds { get; set; }
		[JsonProperty(PropertyName = "fetch_current")]
		public int FetchCurrent { get; set; }
	}
}
