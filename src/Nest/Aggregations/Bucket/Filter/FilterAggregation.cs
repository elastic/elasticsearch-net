using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;

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
			Assign(a => a.Filter = selector?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
