using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<TriggerEventContainer>))]
	public interface ITriggerEventContainer
	{
		[JsonProperty("schedule")]
		IScheduleTriggerEvent Schedule { get; set; }
	}

	[JsonObject]
	public class TriggerEventContainer : ITriggerEventContainer, IDescriptor
	{
		public TriggerEventContainer() {}

		public TriggerEventContainer(TriggerEventBase trigger)
		{
			trigger.ThrowIfNull(nameof(trigger));
			trigger.WrapInContainer(this);
		}

		IScheduleTriggerEvent ITriggerEventContainer.Schedule { get; set; }
	}

	public class TriggerEventDescriptor : TriggerEventContainer
	{
		private TriggerEventDescriptor Assign(Action<ITriggerEventContainer> assigner) => Fluent.Assign(this, assigner);

		public TriggerEventDescriptor Schedule(Func<ScheduleTriggerEventDescriptor, IScheduleTriggerEvent> selector) =>
			Assign(a => a.Schedule = selector(new ScheduleTriggerEventDescriptor()));
	}
}
