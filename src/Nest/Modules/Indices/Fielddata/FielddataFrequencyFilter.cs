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

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(FielddataFrequencyFilter))]
	public interface IFielddataFrequencyFilter
	{
		[DataMember(Name ="max")]
		double? Max { get; set; }

		[DataMember(Name ="min")]
		double? Min { get; set; }

		[DataMember(Name ="min_segment_size")]
		int? MinSegmentSize { get; set; }
	}

	public class FielddataFrequencyFilter : IFielddataFrequencyFilter
	{
		public double? Max { get; set; }
		public double? Min { get; set; }
		public int? MinSegmentSize { get; set; }
	}

	public class FielddataFrequencyFilterDescriptor
		: DescriptorBase<FielddataFrequencyFilterDescriptor, IFielddataFrequencyFilter>, IFielddataFrequencyFilter
	{
		double? IFielddataFrequencyFilter.Max { get; set; }
		double? IFielddataFrequencyFilter.Min { get; set; }
		int? IFielddataFrequencyFilter.MinSegmentSize { get; set; }

		public FielddataFrequencyFilterDescriptor Min(double? min) => Assign(min, (a, v) => a.Min = v);

		public FielddataFrequencyFilterDescriptor Max(double? max) => Assign(max, (a, v) => a.Max = v);

		public FielddataFrequencyFilterDescriptor MinSegmentSize(int? minSegmentSize) => Assign(minSegmentSize, (a, v) => a.MinSegmentSize = v);
	}
}
