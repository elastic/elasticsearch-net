using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class PercolateStats
	{
		[JsonProperty("total")]
		public long Total { get; set; }

		[JsonProperty("time")]
		public string Time { get; set; }
		[JsonProperty("time_in_millis")]
		public long TimeInMilliseconds { get; set; }

		[JsonProperty("current")]
		public long Current { get; set; }

		[JsonProperty("memory_size")]
		public string MemorySize { get; set; }
		[JsonProperty("memory_size_in_bytes")]
		public long MemorySizeInBytes { get; set; }

		[JsonProperty("queries")]
		public long Queries { get; set; }
	}
}
