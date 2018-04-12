using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardDocs
	{
		[JsonProperty("count")]
		public long Count { get; internal set; }

		[JsonProperty("deleted")]
		public long Deleted { get; internal set; }
	}
}
