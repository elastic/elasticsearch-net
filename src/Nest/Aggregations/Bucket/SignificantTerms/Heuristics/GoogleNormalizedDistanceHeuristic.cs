using Newtonsoft.Json;

namespace Nest
{
	public class GoogleNormalizedDistanceHeuristic
	{
		[JsonProperty("background_is_superset")]
		public bool? BackgroundIsSuperSet { get; set; }
	}
}