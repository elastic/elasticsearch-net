using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface ITriggerEvent { }

	public abstract class TriggerEventBase : ITriggerEvent
	{
		public static implicit operator TriggerEventContainer(TriggerEventBase trigger) => trigger == null
			? null
			: new TriggerEventContainer(trigger);

		internal abstract void WrapInContainer(ITriggerEventContainer container);
	}
}
