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
