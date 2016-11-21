using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReserializeJsonConverter<TriggerContainer, ITriggerContainer>))]
	public interface ITriggerContainer
	{
		[JsonProperty("schedule")]
		IScheduleContainer Schedule { get; set; }
	}

	public class TriggerContainer : ITriggerContainer, IDescriptor
	{
		IScheduleContainer ITriggerContainer.Schedule { get; set; }

		public TriggerContainer() {}

		public TriggerContainer(TriggerBase trigger)
		{
			trigger.ThrowIfNull(nameof(trigger));
			trigger.WrapInContainer(this);
		}
	}

	public class TriggerDescriptor : TriggerContainer
	{
		private TriggerDescriptor Assign(Action<ITriggerContainer> assigner) => Fluent.Assign(this, assigner);

		public TriggerDescriptor Schedule(Func<ScheduleDescriptor, IScheduleContainer> selector) =>
			Assign(a => a.Schedule = selector?.InvokeOrDefault(new ScheduleDescriptor()));
	}
}
