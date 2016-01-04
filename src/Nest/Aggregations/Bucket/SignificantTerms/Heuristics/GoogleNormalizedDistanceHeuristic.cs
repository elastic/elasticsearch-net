using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<GoogleNormalizedDistanceHeuristic>))]
	public interface IGoogleNormalizedDistanceHeuristic 
	{
		[JsonProperty("background_is_superset")]
		bool? BackgroundIsSuperSet { get; set; }
	}

	public class GoogleNormalizedDistanceHeuristic : IGoogleNormalizedDistanceHeuristic
	{
		public bool? BackgroundIsSuperSet { get; set; }
	}

	public class GoogleNormalizedDistanceHeuristicDescriptor 
		: DescriptorBase<GoogleNormalizedDistanceHeuristicDescriptor, IGoogleNormalizedDistanceHeuristic>, IGoogleNormalizedDistanceHeuristic
	{
		bool? IGoogleNormalizedDistanceHeuristic.BackgroundIsSuperSet { get; set; }

		public GoogleNormalizedDistanceHeuristicDescriptor BackgroundIsSuperSet(bool backroundIsSuperSet = true) =>
			Assign(a => a.BackgroundIsSuperSet = backroundIsSuperSet);
	}
}