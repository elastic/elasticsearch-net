using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject]
	public interface ITriggerEvent {}

	public abstract class TriggerEventBase : ITriggerEvent
	{
		public static implicit operator TriggerEventContainer(TriggerEventBase trigger) => trigger == null
			? null
			: new TriggerEventContainer(trigger);

		internal abstract void WrapInContainer(ITriggerEventContainer container);
	}
}
