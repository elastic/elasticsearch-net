using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<PercentageScoreHeuristic>))]
	public interface IPercentageScoreHeuristic { }

	public class PercentageScoreHeuristic { }

	public class PercentageScoreHeuristicDescriptor 
		: DescriptorBase<PercentageScoreHeuristicDescriptor, IPercentageScoreHeuristic>, IPercentageScoreHeuristic
	{ }
}