using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class TranslogStats
	{
		[JsonProperty("operations")]
		public long Operations  { get; set; }

		[JsonProperty("uncommitted_operations")]
		public long UncommittedOperations  { get; set; }

		[JsonProperty("size")]
		public string Size { get; set; }

		[JsonProperty("size_in_bytes")]
		public long SizeInBytes  { get; set; }

		[JsonProperty("uncommitted_size")]
		public string UncommittedSize { get; set; }

		[JsonProperty("uncommitted_size_in_bytes")]
		public long UncommittedSizeInBytes  { get; set; }

		[JsonProperty("earliest_last_modified_age")]
		public long EarliestLastModifiedAge  { get; set; }

	}
}
