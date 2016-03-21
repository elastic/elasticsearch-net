using Newtonsoft.Json;

namespace Nest
{
	public class QueryBreakdown
	{
		[JsonProperty("score")]
		public long Score { get; internal set; }

		[JsonProperty("next_doc")]
		public long NextDoc { get; internal set; }

		[JsonProperty("create_weight")]
		public long CreateWeight { get; internal set; }

		[JsonProperty("build_scorer")]
		public long BuildScorer { get; internal set; }

		[JsonProperty("advance")]
		public long Advance { get; internal set; }

		[JsonProperty("match")]
		public long Match { get; internal set; }
	}
}
