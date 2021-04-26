/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(ScheduleTriggerEvent))]
	public interface IScheduleTriggerEvent : ITriggerEvent
	{
		[DataMember(Name = "scheduled_time")]
		Union<DateTimeOffset, string> ScheduledTime { get; set; }

		[DataMember(Name = "triggered_time")]
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
