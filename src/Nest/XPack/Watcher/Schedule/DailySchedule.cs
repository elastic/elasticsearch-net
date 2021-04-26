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
using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(TimeOfDay))]
	public interface ITimeOfDay
	{
		[DataMember(Name ="hour")]
		IEnumerable<int> Hour { get; set; }

		[DataMember(Name ="minute")]
		IEnumerable<int> Minute { get; set; }
	}

	public class TimeOfDay : ITimeOfDay
	{
		public IEnumerable<int> Hour { get; set; }
		public IEnumerable<int> Minute { get; set; }
	}

	public class TimeOfDayDescriptor : DescriptorBase<TimeOfDayDescriptor, ITimeOfDay>, ITimeOfDay
	{
		IEnumerable<int> ITimeOfDay.Hour { get; set; }
		IEnumerable<int> ITimeOfDay.Minute { get; set; }

		public TimeOfDayDescriptor Hour(IEnumerable<int> hours) => Assign(hours, (a, v) => a.Hour = v);

		public TimeOfDayDescriptor Hour(params int[] hours) => Assign(hours, (a, v) => a.Hour = v);

		public TimeOfDayDescriptor Minute(IEnumerable<int> minutes) => Assign(minutes, (a, v) => a.Minute = v);

		public TimeOfDayDescriptor Minute(params int[] minutes) => Assign(minutes, (a, v) => a.Minute = v);
	}

	[InterfaceDataContract]
	[ReadAs(typeof(DailySchedule))]
	public interface IDailySchedule : ISchedule
	{
		[DataMember(Name ="at")]
		Union<IEnumerable<string>, ITimeOfDay> At { get; set; }
	}

	public class DailySchedule : ScheduleBase, IDailySchedule
	{
		public Union<IEnumerable<string>, ITimeOfDay> At { get; set; }

		internal override void WrapInContainer(IScheduleContainer container) => container.Daily = this;
	}

	public class DailyScheduleDescriptor : DescriptorBase<DailyScheduleDescriptor, IDailySchedule>, IDailySchedule
	{
		Union<IEnumerable<string>, ITimeOfDay> IDailySchedule.At { get; set; }

		public DailyScheduleDescriptor At(Func<TimeOfDayDescriptor, ITimeOfDay> selector) =>
			Assign(selector, (a, v) => a.At = new Union<IEnumerable<string>, ITimeOfDay>(v?.InvokeOrDefault(new TimeOfDayDescriptor())));

		public DailyScheduleDescriptor At(IEnumerable<string> times) =>
			Assign(new Union<IEnumerable<string>, ITimeOfDay>(times), (a, v) => a.At = v);

		public DailyScheduleDescriptor At(params string[] times) =>
			Assign(new Union<IEnumerable<string>, ITimeOfDay>(times), (a, v) => a.At = v);
	}
}
