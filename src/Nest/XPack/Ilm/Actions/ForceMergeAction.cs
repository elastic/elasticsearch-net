using Newtonsoft.Json;

namespace Nest
{
	public interface IForceMergeLifecycleAction : ILifecycleAction
	{
		[JsonProperty("max_num_segments")]
		int? MaximumNumberSegments { get; set; }
	}

	public class ForceMergeLifecycleAction : IForceMergeLifecycleAction
	{
		public int? MaximumNumberSegments { get; set; }
	}

	public class ForceMergeLifecycleActionDescriptor : DescriptorBase<ForceMergeLifecycleActionDescriptor, IForceMergeLifecycleAction>, IForceMergeLifecycleAction
	{
		int? IForceMergeLifecycleAction.MaximumNumberSegments { get; set; }

		public ForceMergeLifecycleActionDescriptor MaximumNumberSegments(int? maximumNumberSegments)
			=> Assign(maximumNumberSegments, (a, v) => a.MaximumNumberSegments = maximumNumberSegments);
	}
}
