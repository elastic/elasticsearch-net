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
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(SerialDifferencingAggregation))]
	public interface ISerialDifferencingAggregation : IPipelineAggregation
	{
		[DataMember(Name ="lag")]
		int? Lag { get; set; }
	}

	public class SerialDifferencingAggregation : PipelineAggregationBase, ISerialDifferencingAggregation
	{
		internal SerialDifferencingAggregation() { }

		public SerialDifferencingAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		public int? Lag { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.SerialDifferencing = this;
	}

	public class SerialDifferencingAggregationDescriptor
		: PipelineAggregationDescriptorBase<SerialDifferencingAggregationDescriptor, ISerialDifferencingAggregation, SingleBucketsPath>
			, ISerialDifferencingAggregation
	{
		int? ISerialDifferencingAggregation.Lag { get; set; }

		public SerialDifferencingAggregationDescriptor Lag(int? lag) => Assign(lag, (a, v) => a.Lag = v);
	}
}
