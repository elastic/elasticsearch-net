// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(FielddataFilter))]
	public interface IFielddataFilter
	{
		[DataMember(Name ="frequency")]
		IFielddataFrequencyFilter Frequency { get; set; }

		[DataMember(Name ="regex")]
		IFielddataRegexFilter Regex { get; set; }
	}

	public class FielddataFilter : IFielddataFilter
	{
		public IFielddataFrequencyFilter Frequency { get; set; }
		public IFielddataRegexFilter Regex { get; set; }
	}

	public class FielddataFilterDescriptor
		: DescriptorBase<FielddataFilterDescriptor, IFielddataFilter>, IFielddataFilter
	{
		IFielddataFrequencyFilter IFielddataFilter.Frequency { get; set; }
		IFielddataRegexFilter IFielddataFilter.Regex { get; set; }

		public FielddataFilterDescriptor Frequency(
			Func<FielddataFrequencyFilterDescriptor, IFielddataFrequencyFilter> frequencyFilterSelector
		) =>
			Assign(frequencyFilterSelector(new FielddataFrequencyFilterDescriptor()), (a, v) => a.Frequency = v);

		public FielddataFilterDescriptor Regex(
			Func<FielddataRegexFilterDescriptor, IFielddataRegexFilter> regexFilterSelector
		) =>
			Assign(regexFilterSelector(new FielddataRegexFilterDescriptor()), (a, v) => a.Regex = v);
	}
}
