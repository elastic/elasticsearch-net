// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(IpRangeAggregationRange))]
	public interface IIpRangeAggregationRange
	{
		[DataMember(Name ="from")]
		string From { get; set; }

		[DataMember(Name ="mask")]
		string Mask { get; set; }

		[DataMember(Name ="to")]
		string To { get; set; }
	}

	public class IpRangeAggregationRange : IIpRangeAggregationRange
	{
		public string From { get; set; }

		public string Mask { get; set; }

		public string To { get; set; }
	}

	public class IpRangeAggregationRangeDescriptor
		: DescriptorBase<IpRangeAggregationRangeDescriptor, IIpRangeAggregationRange>, IIpRangeAggregationRange
	{
		string IIpRangeAggregationRange.From { get; set; }
		string IIpRangeAggregationRange.Mask { get; set; }
		string IIpRangeAggregationRange.To { get; set; }

		public IpRangeAggregationRangeDescriptor From(string from) => Assign(from, (a, v) => a.From = v);

		public IpRangeAggregationRangeDescriptor To(string to) => Assign(to, (a, v) => a.To = v);

		public IpRangeAggregationRangeDescriptor Mask(string mask) => Assign(mask, (a, v) => a.Mask = v);
	}
}
