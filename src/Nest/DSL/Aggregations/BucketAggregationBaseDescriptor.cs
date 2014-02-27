using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest.DSL.Aggregations
{
	public abstract class BucketAggregationBaseDescriptor<TBucketAggregation, T>: IAggregationDescriptor
		where TBucketAggregation : BucketAggregationBaseDescriptor<TBucketAggregation, T>
		where T : class
	{
		[JsonProperty("aggs")] 
		internal AggregationDescriptor<T> _Aggregations;

		public TBucketAggregation Aggregations(Func<AggregationDescriptor<T>, AggregationDescriptor<T>> selector)
		{
			this._Aggregations = selector(new AggregationDescriptor<T>());
			return (TBucketAggregation)this;
		}
	}

	public class FilterAggregationDescriptor<T> : BucketAggregationBaseDescriptor<FilterAggregationDescriptor<T>, T>
		where T : class
	{
		[JsonProperty("filter")]
		internal BaseFilter _Filter { get; set; }

		public FilterAggregationDescriptor<T> Filter(Func<FilterDescriptor<T>, BaseFilter> selector)
		{
			this._Filter = selector(new FilterDescriptor<T>());
			return this;
		}
	}
	public class GlobalAggregationDescriptor<T> : BucketAggregationBaseDescriptor<GlobalAggregationDescriptor<T>, T>
		where T : class
	{
		[JsonProperty("global")] internal readonly object _Global = new object {};
	}
}