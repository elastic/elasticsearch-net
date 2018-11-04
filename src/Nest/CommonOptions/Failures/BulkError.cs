using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class BulkError
	{
		[JsonProperty("caused_by")]
		public CausedBy CausedBy { get; internal set; }

		[JsonProperty("index")]
		public string Index { get; internal set; }

		[JsonProperty("reason")]
		public string Reason { get; internal set; }

		[JsonProperty("shard")]
		public int Shard { get; internal set; }

		[JsonProperty("type")]
		public string Type { get; internal set; }

		public override string ToString()
		{
			var cause = CausedBy != null ? $" CausedBy:\n{CausedBy}" : string.Empty;

			return $"Type: {Type} Reason: \"{Reason}\"{cause}";
		}
	}
}
