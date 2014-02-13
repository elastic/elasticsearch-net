using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class TypeStats
	{
		[JsonProperty(PropertyName = "index_total")]
		public long Total { get; set; }
		[JsonProperty(PropertyName = "index_time")]
		public string Time { get; set; }
		[JsonProperty(PropertyName = "index_time_in_millis")]
		public double TimeInMilliseconds { get; set; }
		[JsonProperty(PropertyName = "index_current")]
		public int Current { get; set; }
		[JsonProperty(PropertyName = "delete_total")]
		public int DeleteTotal { get; set; }
		[JsonProperty(PropertyName = "delete_time")]
		public string DeleteTime { get; set; }
		[JsonProperty(PropertyName = "delete_time_in_millis")]
		public double DeleteTimeInMilliseconds { get; set; }
		[JsonProperty(PropertyName = "delete_current")]
		public long DeleteCurrent { get; set; }
	}
}
