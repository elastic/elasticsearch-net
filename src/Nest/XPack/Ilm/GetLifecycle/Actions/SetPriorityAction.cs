using Newtonsoft.Json;

namespace Nest
{
	public class SetPriorityAction : LifecycleActionBase
	{
		public SetPriorityAction() : base("set_priority"){ }

		[JsonProperty("priority")]
		public int Priority { get; internal set; }
	}
}
