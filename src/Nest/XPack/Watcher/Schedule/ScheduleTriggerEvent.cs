using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<ScheduleTriggerEvent>))]
	public interface IScheduleTriggerEvent : ITriggerEvent
	{
		[JsonProperty("scheduled_time")]
		Union<DateTimeOffset, string> ScheduledTime { get; set; }

		[JsonProperty("triggered_time")]
		Union<DateTimeOffset, string> TriggeredTime { get; set; }
	}

	public class ScheduleTriggerEvent : TriggerEventBase, IScheduleTriggerEvent
	{
		public Union<DateTimeOffset, string> ScheduledTime { get; set; }
		public Union<DateTimeOffset, string> TriggeredTime { get; set; }

		internal override void WrapInContainer(ITriggerEventContainer container) => container.Schedule = this;
	}

	public class ScheduleTriggerEventDescriptor
		: DescriptorBase<ScheduleTriggerEventDescriptor, IScheduleTriggerEvent>, IScheduleTriggerEvent
	{
		Union<DateTimeOffset, string> IScheduleTriggerEvent.ScheduledTime { get; set; }
		Union<DateTimeOffset, string> IScheduleTriggerEvent.TriggeredTime { get; set; }

		public ScheduleTriggerEventDescriptor TriggeredTime(DateTimeOffset? triggeredTime) =>
			Assign(triggeredTime, (a, v) => a.TriggeredTime = v);

		public ScheduleTriggerEventDescriptor TriggeredTime(string triggeredTime) =>
			Assign(triggeredTime, (a, v) => a.TriggeredTime = v);

		public ScheduleTriggerEventDescriptor ScheduledTime(DateTimeOffset? scheduledTime) =>
			Assign(scheduledTime, (a, v) => a.ScheduledTime = v);

		public ScheduleTriggerEventDescriptor ScheduledTime(string scheduledTime) =>
			Assign(scheduledTime, (a, v) => a.ScheduledTime = v);
	}
}
