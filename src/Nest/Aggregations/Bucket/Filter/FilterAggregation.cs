using System;
using Nest.Resolvers.Converters.Aggregations;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(FilterAggregatorConverter))]
	public interface IFilterAggregator : IBucketAggregator
	{
		IQueryContainer Filter { get; set; }
	}

	public class FilterAggregator : BucketAggregator, IFilterAggregator
	{
		public IQueryContainer Filter { get; set; }
	}

	public class FilterAggregationDescriptor<T> 
		: BucketAggregatorBaseDescriptor<FilterAggregationDescriptor<T>,IFilterAggregator, T> 
			, IFilterAggregator 
		where T : class
	{
		IQueryContainer IFilterAggregator.Filter { get; set; }

		public FilterAggregationDescriptor<T> Filter(Func<QueryDescriptor<T>, QueryContainer> selector) =>
			Assign(a=> a.Filter = selector?.Invoke(new QueryDescriptor<T>()));

	}
}