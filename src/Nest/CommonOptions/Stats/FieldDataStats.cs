using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class FielddataStats
	{
		[JsonProperty("evictions")]
		public long Evictions { get; set; }

		[JsonProperty("memory_size")]
		public string MemorySize { get; set; }
		[JsonProperty("memory_size_in_bytes")]
		public long MemorySizeInBytes { get; set; }

	}
}
