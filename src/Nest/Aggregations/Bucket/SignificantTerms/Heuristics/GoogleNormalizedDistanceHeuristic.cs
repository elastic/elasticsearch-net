using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(GoogleNormalizedDistanceHeuristic))]
	public interface IGoogleNormalizedDistanceHeuristic
	{
		[DataMember(Name ="background_is_superset")]
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

		public GoogleNormalizedDistanceHeuristicDescriptor BackgroundIsSuperSet(bool? backroundIsSuperSet = true) =>
			Assign(a => a.BackgroundIsSuperSet = backroundIsSuperSet);
	}
}
