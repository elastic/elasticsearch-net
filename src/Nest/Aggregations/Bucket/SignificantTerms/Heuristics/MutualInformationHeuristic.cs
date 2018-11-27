using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(MutualInformationHeuristic))]
	public interface IMutualInformationHeuristic
	{
		[DataMember(Name ="background_is_superset")]
		bool? BackgroundIsSuperSet { get; set; }

		[DataMember(Name ="include_negatives")]
		bool? IncludeNegatives { get; set; }
	}

	public class MutualInformationHeuristic : IMutualInformationHeuristic
	{
		public bool? BackgroundIsSuperSet { get; set; }
		public bool? IncludeNegatives { get; set; }
	}

	public class MutualInformationHeuristicDescriptor
		: DescriptorBase<MutualInformationHeuristicDescriptor, IMutualInformationHeuristic>, IMutualInformationHeuristic
	{
		bool? IMutualInformationHeuristic.BackgroundIsSuperSet { get; set; }
		bool? IMutualInformationHeuristic.IncludeNegatives { get; set; }

		public MutualInformationHeuristicDescriptor IncludeNegatives(bool? includeNegatives = true) =>
			Assign(a => a.IncludeNegatives = includeNegatives);

		public MutualInformationHeuristicDescriptor BackgroundIsSuperSet(bool? backgroundIsSuperSet = true) =>
			Assign(a => a.BackgroundIsSuperSet = backgroundIsSuperSet);
	}
}
