using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class BulkError
	{
		[JsonProperty("index")]
		public string Index { get; internal set; }

		[JsonProperty("shard")]
		public int Shard { get; internal set; }

		[JsonProperty("type")]
		public string Type { get; internal set; }

		[JsonProperty("reason")]
		public string Reason { get; internal set; }

		public override string ToString() => $"Type: {Type} Reason: \"{Reason}\"";
	}
}