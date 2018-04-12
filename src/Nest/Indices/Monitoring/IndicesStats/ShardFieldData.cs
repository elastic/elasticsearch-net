using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardFieldData
	{
		[JsonProperty("memory_size_in_bytes")]
		public long MemorySizeInBytes { get; set; }
		[JsonProperty("evictions")]
		public long Evictions { get; set; }
	}
}