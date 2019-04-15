using Newtonsoft.Json;

namespace Nest
{
	public interface IShrinkLifecycleAction : ILifecycleAction
	{
		[JsonProperty("number_of_shards")]
		int NumberOfShards { get; set; }
	}

	public class ShrinkLifecycleAction : IShrinkLifecycleAction
	{
		public int NumberOfShards { get; set; }
	}

	public class ShrinkLifecycleActionDescriptor : DescriptorBase<ShrinkLifecycleActionDescriptor, IShrinkLifecycleAction>, IShrinkLifecycleAction
	{
		int IShrinkLifecycleAction.NumberOfShards { get; set; }

		public ShrinkLifecycleActionDescriptor NumberOfShards(int numberOfShards) => Assign(numberOfShards, (a, v) => a.NumberOfShards = numberOfShards);
	}
}
