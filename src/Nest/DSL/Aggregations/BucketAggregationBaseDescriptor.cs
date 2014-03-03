using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{

	public interface IBucketAggregationDescriptor<T>
		where T : class
	{
		
		IDictionary<string, AggregationDescriptor<T>> NestedAggregations { get; set; }
	}

	public abstract class BucketAggregationBaseDescriptor<TBucketAggregation, T> 
		: IAggregationDescriptor, IBucketAggregationDescriptor<T> 
		where TBucketAggregation : BucketAggregationBaseDescriptor<TBucketAggregation, T>
		where T : class
	{
		IDictionary<string, AggregationDescriptor<T>> IBucketAggregationDescriptor<T>.NestedAggregations { get; set; }

		public TBucketAggregation Aggregations(Func<AggregationDescriptor<T>, AggregationDescriptor<T>> selector)
		{	
			var aggs = selector(new AggregationDescriptor<T>());
			if (aggs == null) return (TBucketAggregation)this;
			((IBucketAggregationDescriptor<T>)this).NestedAggregations = aggs._Aggregations;
			return (TBucketAggregation)this;
		}
	}
}