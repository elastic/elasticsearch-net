// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

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
