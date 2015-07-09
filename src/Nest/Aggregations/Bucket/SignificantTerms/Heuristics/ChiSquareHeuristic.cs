using Newtonsoft.Json;

namespace Nest
{
	public class ChiSquareHeuristic
	{
		[JsonProperty("include_negatives")]
		public bool? IncludeNegatives { get; set; }
		[JsonProperty("background_is_superset")]
		public bool? BackgroundIsSuperSet { get; set; }
	}
}