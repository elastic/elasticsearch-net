using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardCompletion
	{
		[JsonProperty("size_in_bytes")]
		public long SizeInBytes { get; internal set; }
	}
}
