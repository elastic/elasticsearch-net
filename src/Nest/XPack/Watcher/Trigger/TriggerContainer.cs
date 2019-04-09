using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(ReserializeJsonConverter<TriggerContainer, ITriggerContainer>))]
	public interface ITriggerContainer
	{
		[JsonProperty("schedule")]
		IScheduleContainer Schedule { get; set; }
	}

	public class TriggerContainer : ITriggerContainer, IDescriptor
	{
		public TriggerContainer() { }

		public TriggerContainer(TriggerBase trigger)
		{
			trigger.ThrowIfNull(nameof(trigger));
			trigger.WrapInContainer(this);
		}

		IScheduleContainer ITriggerContainer.Schedule { get; set; }
	}

	public class TriggerDescriptor : TriggerContainer
	{
		private TriggerDescriptor Assign<TValue>(TValue value, Action<ITriggerContainer, TValue> assigner) => Fluent.Assign(this, value, assigner);

		public TriggerDescriptor Schedule(Func<ScheduleDescriptor, IScheduleContainer> selector) =>
			Assign(selector, (a, v) => a.Schedule = v?.InvokeOrDefault(new ScheduleDescriptor()));
	}
}
