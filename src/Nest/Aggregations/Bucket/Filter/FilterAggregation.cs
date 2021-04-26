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
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(FilterAggregationFormatter))]
	public interface IFilterAggregation : IBucketAggregation
	{
		[DataMember(Name ="filter")]
		QueryContainer Filter { get; set; }
	}

	public class FilterAggregation : BucketAggregationBase, IFilterAggregation
	{
		internal FilterAggregation() { }

		public FilterAggregation(string name) : base(name) { }

		public QueryContainer Filter { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.Filter = this;
	}

	public class FilterAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<FilterAggregationDescriptor<T>, IFilterAggregation, T>
			, IFilterAggregation
		where T : class
	{
		QueryContainer IFilterAggregation.Filter { get; set; }

		public FilterAggregationDescriptor<T> Filter(Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(selector, (a, v) => a.Filter = v?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
