using Newtonsoft.Json;

namespace Nest
{
	public interface ISetPriorityLifecycleAction : ILifecycleAction
	{
		[JsonProperty("priority")]
		int? Priority { get; set; }
	}

	public class SetPriorityLifecycleAction : ISetPriorityLifecycleAction
	{
		public int? Priority { get; set; }
	}

	public class SetPriorityLifecycleActionDescriptor : DescriptorBase<SetPriorityLifecycleActionDescriptor, ISetPriorityLifecycleAction>, ISetPriorityLifecycleAction
	{
		int? ISetPriorityLifecycleAction.Priority { get; set; }

		public SetPriorityLifecycleActionDescriptor Priority(int? priority) => Assign(priority, (a, v) => a.Priority = priority);
	}
}
