using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class MergesStats
	{
	
		[JsonProperty(PropertyName = "current")]
		public long Current { get; set; }
		[JsonProperty(PropertyName = "current_docs")]
		public long CurrentDocuments { get; set; }
		[JsonProperty(PropertyName = "current_size")]
		public string CurrentSize { get; set; }
		[JsonProperty(PropertyName = "current_size_in_bytes")]
		public double CurrentSizeInBytes { get; set; }

		[JsonProperty(PropertyName = "total")]
		public long Total { get; set; }
		[JsonProperty(PropertyName = "total_time")]
		public string TotalTime { get; set; }
		[JsonProperty(PropertyName = "total_time_in_millis")]
		public double TotalTimeInMilliseconds { get; set; }

		[JsonProperty(PropertyName = "total_docs")]
		public long TotalDocuments { get; set; }
		[JsonProperty(PropertyName = "total_size")]
		public string TotalSize { get; set; }
		[JsonProperty(PropertyName = "total_size_in_bytes")]
		public string TotalSizeInBytes { get; set; }
	}
}
