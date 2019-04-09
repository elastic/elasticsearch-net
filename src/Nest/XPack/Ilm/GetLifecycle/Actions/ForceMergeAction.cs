using Newtonsoft.Json;

namespace Nest
{
	public class ForceMergeAction : LifecycleActionBase
	{
		public ForceMergeAction() : base("forcemerge"){ }

		[JsonProperty("max_num_segments")]
		public int MaximumNumberSegments { get; internal set; }
	}
}
