using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class MutualInformationHeuristic
	{
		[JsonProperty("include_negatives")]
		public bool? IncludeNegatives { get; set; }
		[JsonProperty("background_is_superset")]
		public bool? BackgroundIsSuperSet { get; set; }
	}

	public class ChiSquareHeuristic
	{
		[JsonProperty("include_negatives")]
		public bool? IncludeNegatives { get; set; }
		[JsonProperty("background_is_superset")]
		public bool? BackgroundIsSuperSet { get; set; }
	}

	public class GoogleNormalizedDistanceHeuristic
	{
		[JsonProperty("background_is_superset")]
		public bool? BackgroundIsSuperSet { get; set; }
	}
	
}