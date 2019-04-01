using System;
using System.Runtime.Serialization;

namespace Nest
{
	[ReadAs(typeof(TriggerEventContainer))]
	public interface ITriggerEventContainer
	{
		[DataMember(Name = "schedule")]
		IScheduleTriggerEvent Schedule { get; set; }
	}

	[DataContract]
	public class TriggerEventContainer : ITriggerEventContainer, IDescriptor
	{
		public TriggerEventContainer() { }

		public TriggerEventContainer(TriggerEventBase trigger)
		{
			trigger.ThrowIfNull(nameof(trigger));
			trigger.WrapInContainer(this);
		}

		IScheduleTriggerEvent ITriggerEventContainer.Schedule { get; set; }
	}

	public class TriggerEventDescriptor : TriggerEventContainer
	{
		private TriggerEventDescriptor Assign<TValue>(TValue value, Action<ITriggerEventContainer, TValue> assigner) => Fluent.Assign(this, value, assigner);

		public TriggerEventDescriptor Schedule(Func<ScheduleTriggerEventDescriptor, IScheduleTriggerEvent> selector) =>
			Assign(selector, (a, v) => a.Schedule = v?.Invoke(new ScheduleTriggerEventDescriptor()));
	}
}
