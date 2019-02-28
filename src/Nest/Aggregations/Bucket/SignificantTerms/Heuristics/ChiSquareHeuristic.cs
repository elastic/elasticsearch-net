using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(ChiSquareHeuristic))]
	public interface IChiSquareHeuristic
	{
		[DataMember(Name ="background_is_superset")]
		bool? BackgroundIsSuperSet { get; set; }

		[DataMember(Name ="include_negatives")]
		bool? IncludeNegatives { get; set; }
	}

	public class ChiSquareHeuristic : IChiSquareHeuristic
	{
		public bool? BackgroundIsSuperSet { get; set; }
		public bool? IncludeNegatives { get; set; }
	}

	public class ChiSquareHeuristicDescriptor
		: DescriptorBase<ChiSquareHeuristicDescriptor, IChiSquareHeuristic>, IChiSquareHeuristic
	{
		bool? IChiSquareHeuristic.BackgroundIsSuperSet { get; set; }
		bool? IChiSquareHeuristic.IncludeNegatives { get; set; }

		public ChiSquareHeuristicDescriptor BackgroundIsSuperSet(bool? backgroundIsSuperSet = true) =>
			Assign(a => a.BackgroundIsSuperSet = backgroundIsSuperSet);

		public ChiSquareHeuristicDescriptor IncludeNegatives(bool? includeNegatives = true) =>
			Assign(a => a.IncludeNegatives = includeNegatives);
	}
}
