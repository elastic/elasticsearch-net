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

using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(TimeOfMonth))]
	public interface ITimeOfMonth
	{
		[DataMember(Name ="at")]
		[JsonFormatter(typeof(SingleOrEnumerableFormatter<string>))]
		IEnumerable<string> At { get; set; }

		[DataMember(Name ="on")]
		[JsonFormatter(typeof(SingleOrEnumerableFormatter<int>))]
		IEnumerable<int> On { get; set; }
	}

	public class TimeOfMonth : ITimeOfMonth
	{
		public TimeOfMonth() { }

		public TimeOfMonth(int on, string at)
		{
			On = new[] { on };
			At = new[] { at };
		}

		public IEnumerable<string> At { get; set; }

		public IEnumerable<int> On { get; set; }
	}

	public class TimeOfMonthDescriptor : DescriptorBase<TimeOfMonthDescriptor, ITimeOfMonth>, ITimeOfMonth
	{
		IEnumerable<string> ITimeOfMonth.At { get; set; }
		IEnumerable<int> ITimeOfMonth.On { get; set; }

		public TimeOfMonthDescriptor On(IEnumerable<int> dates) => Assign(dates, (a, v) => a.On = v);

		public TimeOfMonthDescriptor On(params int[] dates) => Assign(dates, (a, v) => a.On = v);

		public TimeOfMonthDescriptor At(IEnumerable<string> time) => Assign(time, (a, v) => a.At = v);

		public TimeOfMonthDescriptor At(params string[] time) => Assign(time, (a, v) => a.At = v);
	}
}
