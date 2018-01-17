using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<ScheduleTriggerEvent>))]
	public interface IScheduleTriggerEvent : ITriggerEvent
	{
		[JsonProperty("triggered_time")]
		Union<DateTimeOffset, string> TriggeredTime { get; set; }

		[JsonProperty("scheduled_time")]
		Union<DateTimeOffset, string> ScheduledTime { get; set; }
	}

	public class ScheduleTriggerEvent : TriggerEventBase, IScheduleTriggerEvent
	{
		public Union<DateTimeOffset,string> TriggeredTime { get; set; }

		public Union<DateTimeOffset, string> ScheduledTime { get; set; }

		internal override void WrapInContainer(ITriggerEventContainer container) => container.Schedule = this;
	}

	public class ScheduleTriggerEventDescriptor
		: DescriptorBase<ScheduleTriggerEventDescriptor, IScheduleTriggerEvent>, IScheduleTriggerEvent
	{
		Union<DateTimeOffset, string> IScheduleTriggerEvent.TriggeredTime { get; set; }
		Union<DateTimeOffset, string> IScheduleTriggerEvent.ScheduledTime { get; set; }

		public ScheduleTriggerEventDescriptor TriggeredTime(DateTimeOffset? triggeredTime) =>
			Assign(a => a.TriggeredTime = triggeredTime);

		public ScheduleTriggerEventDescriptor TriggeredTime(string triggeredTime) =>
			Assign(a => a.TriggeredTime = triggeredTime);

		public ScheduleTriggerEventDescriptor ScheduledTime(DateTimeOffset? scheduledTime) =>
			Assign(a => a.ScheduledTime = scheduledTime);

		public ScheduleTriggerEventDescriptor ScheduledTime(string scheduledTime) =>
			Assign(a => a.ScheduledTime = scheduledTime);
	}
}
