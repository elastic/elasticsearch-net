using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<MutualInformationHeuristic>))]
	public interface IMutualInformationHeuristic 
	{
		[JsonProperty("include_negatives")]
		bool? IncludeNegatives { get; set; }

		[JsonProperty("background_is_superset")]
		bool? BackgroundIsSuperSet { get; set; }
	}

	public class MutualInformationHeuristic : IMutualInformationHeuristic
	{
		public bool? IncludeNegatives { get; set; }
		public bool? BackgroundIsSuperSet { get; set; }
	}

	public class MutualInformationHeuristicDescriptor 
		: DescriptorBase<MutualInformationHeuristicDescriptor, IMutualInformationHeuristic>, IMutualInformationHeuristic
	{
		bool? IMutualInformationHeuristic.IncludeNegatives { get; set; }
		bool? IMutualInformationHeuristic.BackgroundIsSuperSet { get; set; }

		public MutualInformationHeuristicDescriptor IncludeNegatives(bool includeNegatives = true) =>
			Assign(a => a.IncludeNegatives = includeNegatives);

		public MutualInformationHeuristicDescriptor BackgroundIsSuperSet(bool backgroundIsSuperSet = true) =>
			Assign(a => a.BackgroundIsSuperSet = backgroundIsSuperSet);
	}
}