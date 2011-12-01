using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ElasticSearch.Client.Domain
{
	[JsonObject]
	public class RefreshStats
	{
	
		[JsonProperty(PropertyName = "total")]
		public int Total { get; set; }
		[JsonProperty(PropertyName = "total_time")]
		public string TotalTime { get; set; }
		[JsonProperty(PropertyName = "total_time_in_millis")]
		public double TotalTimeInMilliseconds { get; set; }

	}
}
