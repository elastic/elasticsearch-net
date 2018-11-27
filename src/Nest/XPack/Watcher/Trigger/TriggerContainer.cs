using System;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	[JsonConverter(typeof(ReserializeJsonConverter<TriggerContainer, ITriggerContainer>))]
	public interface ITriggerContainer
	{
		[DataMember(Name ="schedule")]
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
		private TriggerDescriptor Assign(Action<ITriggerContainer> assigner) => Fluent.Assign(this, assigner);

		public TriggerDescriptor Schedule(Func<ScheduleDescriptor, IScheduleContainer> selector) =>
			Assign(a => a.Schedule = selector?.InvokeOrDefault(new ScheduleDescriptor()));
	}
}
