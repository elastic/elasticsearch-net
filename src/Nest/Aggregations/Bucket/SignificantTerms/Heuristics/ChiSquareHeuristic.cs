using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

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
			Assign(backgroundIsSuperSet, (a, v) => a.BackgroundIsSuperSet = v);

		public ChiSquareHeuristicDescriptor IncludeNegatives(bool? includeNegatives = true) =>
			Assign(includeNegatives, (a, v) => a.IncludeNegatives = v);
	}
}
