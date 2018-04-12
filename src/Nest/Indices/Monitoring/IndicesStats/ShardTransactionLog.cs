using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardTransactionLog
	{
		[JsonProperty("operations")]
		public long Operations { get; set; }
		[JsonProperty("size_in_bytes")]
		public long SizeInBytes { get; set; }
		[JsonProperty("uncommitted_operations")]
		public long UncommittedOperations { get; set; }
		[JsonProperty("uncommitted_size_in_bytes")]
		public long UncommittedSizeInBytes { get; set; }
	}
}