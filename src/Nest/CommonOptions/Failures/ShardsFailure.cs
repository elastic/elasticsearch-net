using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class ShardsFailure
	{
		[JsonProperty("index")]
		public string Index { get; internal set; }

		[JsonProperty("shard")]
		public int Shard { get; internal set; }

		[JsonProperty("status")]
		public int Status { get; internal set; }

		[JsonProperty("reason")]
		public string Reason { get; internal set; }

	}
}