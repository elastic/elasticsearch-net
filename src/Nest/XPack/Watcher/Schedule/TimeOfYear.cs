// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(TimeOfYear))]
	public interface ITimeOfYear
	{
		[DataMember(Name ="at")]
		[JsonFormatter(typeof(SingleOrEnumerableFormatter<string>))]
		IEnumerable<string> At { get; set; }

		[DataMember(Name ="int")]
		[JsonFormatter(typeof(SingleOrEnumerableFormatter<Month>))]
		IEnumerable<Month> In { get; set; }

		[DataMember(Name ="on")]
		[JsonFormatter(typeof(SingleOrEnumerableFormatter<int>))]
		IEnumerable<int> On { get; set; }
	}

	public class TimeOfYear : ITimeOfYear
	{
		public IEnumerable<string> At { get; set; }
		public IEnumerable<Month> In { get; set; }

		public IEnumerable<int> On { get; set; }
	}

	public class TimeOfYearDescriptor : DescriptorBase<TimeOfYearDescriptor, ITimeOfYear>, ITimeOfYear
	{
		IEnumerable<string> ITimeOfYear.At { get; set; }
		IEnumerable<Month> ITimeOfYear.In { get; set; }
		IEnumerable<int> ITimeOfYear.On { get; set; }

		public TimeOfYearDescriptor In(IEnumerable<Month> @in) => Assign(@in, (a, v) => a.In = v);

		public TimeOfYearDescriptor In(params Month[] @in) => Assign(@in, (a, v) => a.In = v);

		public TimeOfYearDescriptor On(IEnumerable<int> on) => Assign(on, (a, v) => a.On = v);

		public TimeOfYearDescriptor On(params int[] on) => Assign(on, (a, v) => a.On = v);

		public TimeOfYearDescriptor At(IEnumerable<string> time) => Assign(time, (a, v) => a.At = v);

		public TimeOfYearDescriptor At(params string[] time) => Assign(time, (a, v) => a.At = v);
	}
}
