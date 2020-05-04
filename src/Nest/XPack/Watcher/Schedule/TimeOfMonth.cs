// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

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
