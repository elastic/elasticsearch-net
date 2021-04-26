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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(ScheduleFormatter<IYearlySchedule, YearlySchedule, ITimeOfYear>))]
	public interface IYearlySchedule : ISchedule, IEnumerable<ITimeOfYear> { }

	public class YearlySchedule : ScheduleBase, IYearlySchedule
	{
		private List<ITimeOfYear> _times;

		public YearlySchedule(IEnumerable<ITimeOfYear> times) => _times = times?.ToList();

		public YearlySchedule(params ITimeOfYear[] times) => _times = times?.ToList();

		public YearlySchedule() { }

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public IEnumerator<ITimeOfYear> GetEnumerator() =>
			_times?.GetEnumerator() ?? Enumerable.Empty<ITimeOfYear>().GetEnumerator();

		public void Add(ITimeOfYear time)
		{
			if (_times == null) _times = new List<ITimeOfYear>();
			_times.Add(time);
		}

		internal override void WrapInContainer(IScheduleContainer container) => container.Yearly = this;

		public static implicit operator YearlySchedule(ITimeOfYear[] times) => new YearlySchedule(times);
	}

	public class YearlyScheduleDescriptor : DescriptorPromiseBase<YearlyScheduleDescriptor, YearlySchedule>
	{
		public YearlyScheduleDescriptor() : base(new YearlySchedule()) { }

		public YearlyScheduleDescriptor Add(Func<TimeOfYearDescriptor, ITimeOfYear> selector) =>
			Assign(selector, (a, v) => a.Add(v.InvokeOrDefault(new TimeOfYearDescriptor())));
	}
}
