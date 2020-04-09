using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(PercentageScoreHeuristic))]
	public interface IPercentageScoreHeuristic { }

	public class PercentageScoreHeuristic : IPercentageScoreHeuristic { }

	public class PercentageScoreHeuristicDescriptor
		: DescriptorBase<PercentageScoreHeuristicDescriptor, IPercentageScoreHeuristic>, IPercentageScoreHeuristic { }
}
