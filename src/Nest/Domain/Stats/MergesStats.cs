using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest.Domain
{
	[JsonObject]
	public class MergesStats
	{
	
		[JsonProperty(PropertyName = "current")]
		public int Current { get; set; }
		[JsonProperty(PropertyName = "current_docs")]
		public int CurrentDocuments { get; set; }
		[JsonProperty(PropertyName = "current_size")]
		public string CurrentSize { get; set; }
		[JsonProperty(PropertyName = "current_size_in_bytes")]
		public double CurrentSizeInBytes { get; set; }

		[JsonProperty(PropertyName = "total")]
		public int Total { get; set; }
		[JsonProperty(PropertyName = "total_time")]
		public string TotalTime { get; set; }
		[JsonProperty(PropertyName = "total_time_in_millis")]
		public double TotalTimeInMilliseconds { get; set; }

		[JsonProperty(PropertyName = "total_docs")]
		public int TotalDocuments { get; set; }
		[JsonProperty(PropertyName = "total_size")]
		public string TotalSize { get; set; }
		[JsonProperty(PropertyName = "total_size_in_bytes")]
		public string TotalSizeInBytes { get; set; }
	}
}
