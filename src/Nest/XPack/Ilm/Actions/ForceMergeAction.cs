using Newtonsoft.Json;

namespace Nest
{
	public interface IForceMergeAction : ILifecycleAction
	{
		[JsonProperty("max_num_segments")]
		int MaximumNumberSegments { get; set; }
	}

	public class ForceMergeAction : LifecycleActionBase, IForceMergeAction
	{
		public ForceMergeAction() : base("forcemerge"){ }

		public int MaximumNumberSegments { get; set; }
	}

	public class ForceMergeActionDescriptor : LifecycleActionDescriptorBase<ForceMergeActionDescriptor, IForceMergeAction>, IForceMergeAction
	{
		public ForceMergeActionDescriptor() : base("forcemerge") { }

		int IForceMergeAction.MaximumNumberSegments { get; set; }

		public ForceMergeActionDescriptor MaximumNumberSegments(int maximumNumberSegments)
			=> Assign(maximumNumberSegments, (a, v) => a.MaximumNumberSegments = maximumNumberSegments);
	}
}
