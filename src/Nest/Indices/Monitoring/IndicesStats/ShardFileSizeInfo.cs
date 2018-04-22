using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardFileSizeInfo
	{
		[JsonProperty("size_in_bytes")]
		public long SizeInBytes { get; internal set; }
		[JsonProperty("description")]
		public string Description { get; internal set; }
	}
}
