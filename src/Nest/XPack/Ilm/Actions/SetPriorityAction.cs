using Newtonsoft.Json;

namespace Nest
{
	public interface ISetPriorityAction : ILifecycleAction
	{
		[JsonProperty("priority")]
		int Priority { get; set; }
	}

	public class SetPriorityAction : LifecycleActionBase, ISetPriorityAction
	{
		public SetPriorityAction() : base("set_priority"){ }

		public int Priority { get; set; }
	}

	public class SetPriorityActionDescriptor : LifecycleActionDescriptorBase<SetPriorityActionDescriptor, ISetPriorityAction>, ISetPriorityAction
	{
		public SetPriorityActionDescriptor() : base("set_priority") { }

		int ISetPriorityAction.Priority { get; set; }

		public SetPriorityActionDescriptor Priority(int priority) => Assign(priority, (a, v) => a.Priority = priority);
	}
}
