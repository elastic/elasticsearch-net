namespace Nest
{
	public interface ITrigger {}

	public abstract class TriggerBase : ITrigger
	{
		public static implicit operator TriggerContainer(TriggerBase trigger) => trigger == null
			? null
			: new TriggerContainer(trigger);

		internal abstract void WrapInContainer(ITriggerContainer container);
	}
}
