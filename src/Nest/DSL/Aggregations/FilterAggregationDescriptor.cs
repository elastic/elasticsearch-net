using System;
using Nest.Resolvers.Converters;
using Nest.Resolvers.Converters.Aggregations;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(FilterAggregatorConverter))]
	public interface IFilterAggregator : IBucketAggregator
	{
		IFilterContainer Filter { get; set; }
	}

	public class FilterAggregator : BucketAggregator, IFilterAggregator
	{
		public IFilterContainer Filter { get; set; }
	}

	public class FilterAggregationDescriptor<T> : BucketAggregationBaseDescriptor<FilterAggregationDescriptor<T>, T> , IFilterAggregator 
		where T : class
	{
		IFilterContainer IFilterAggregator.Filter { get; set; }

		public FilterAggregationDescriptor<T> Filter(Func<FilterDescriptor<T>, FilterContainer> selector)
		{
			((IFilterAggregator)this).Filter = selector(new FilterDescriptor<T>());
			return this;
		}


	}
}