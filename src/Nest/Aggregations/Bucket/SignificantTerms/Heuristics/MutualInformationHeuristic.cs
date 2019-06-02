using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

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
			Assign(includeNegatives, (a, v) => a.IncludeNegatives = v);

		public MutualInformationHeuristicDescriptor BackgroundIsSuperSet(bool? backgroundIsSuperSet = true) =>
			Assign(backgroundIsSuperSet, (a, v) => a.BackgroundIsSuperSet = v);
	}
}
