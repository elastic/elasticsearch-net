namespace Nest
{
	public abstract class TriggerBase
	{
		public static implicit operator TriggerContainer(TriggerBase trigger) => trigger == null
			? null
			: new TriggerContainer(trigger);

		internal abstract void WrapInContainer(ITriggerContainer container);
	}
}
