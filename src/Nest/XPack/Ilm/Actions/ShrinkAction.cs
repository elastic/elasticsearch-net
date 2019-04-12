using Newtonsoft.Json;

namespace Nest
{
	public interface IShrinkAction : ILifecycleAction
	{
		[JsonProperty("number_of_shards")]
		int NumberOfShards { get; set; }
	}

	public class ShrinkAction : LifecycleActionBase, IShrinkAction
	{
		public ShrinkAction() : base("shrink"){ }

		public int NumberOfShards { get; set; }
	}

	public class ShrinkActionDescriptor : LifecycleActionDescriptorBase<ShrinkActionDescriptor, IShrinkAction>, IShrinkAction
	{
		public ShrinkActionDescriptor() : base("shrink") { }

		int IShrinkAction.NumberOfShards { get; set; }

		public ShrinkActionDescriptor NumberOfShards(int numberOfShards) => Assign(numberOfShards, (a, v) => a.NumberOfShards = numberOfShards);
	}
}
