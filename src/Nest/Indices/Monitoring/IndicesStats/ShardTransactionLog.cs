using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardTransactionLog
	{
		[JsonProperty("operations")]
		public long Operations { get; internal set; }
		[JsonProperty("size_in_bytes")]
		public long SizeInBytes { get; internal set; }
		[JsonProperty("uncommitted_operations")]
		public long UncommittedOperations { get; internal set; }
		[JsonProperty("uncommitted_size_in_bytes")]
		public long UncommittedSizeInBytes { get; internal set; }
	}
}
