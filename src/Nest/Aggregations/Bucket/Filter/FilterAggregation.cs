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

	public class FilterAggregatorDescriptor<T> 
		: BucketAggregatorBaseDescriptor<FilterAggregatorDescriptor<T>,IFilterAggregator, T> 
			, IFilterAggregator 
		where T : class
	{
		IQueryContainer IFilterAggregator.Filter { get; set; }

		public FilterAggregatorDescriptor<T> Filter(Func<QueryDescriptor<T>, QueryContainer> selector) =>
			Assign(a=> a.Filter = selector?.Invoke(new QueryDescriptor<T>()));

	}
}