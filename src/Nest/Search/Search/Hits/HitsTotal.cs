using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class HitsTotal
	{
//		[JsonProperty("max_score")]
//		public double MaxScore { get; internal set; }

		[JsonProperty("value")]
		public long? Value { get; internal set; }

	}
}
