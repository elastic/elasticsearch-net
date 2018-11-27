using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(PercentageScoreHeuristic))]
	public interface IPercentageScoreHeuristic { }

	public class PercentageScoreHeuristic { }

	public class PercentageScoreHeuristicDescriptor
		: DescriptorBase<PercentageScoreHeuristicDescriptor, IPercentageScoreHeuristic>, IPercentageScoreHeuristic { }
}
