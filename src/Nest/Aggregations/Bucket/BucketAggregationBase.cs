using System;
using System.Collections.Generic;

namespace Nest
{
	public abstract class BucketAggregationBase : AggregationsHelper , IBucketAggregation
	{
		protected BucketAggregationBase() { }
		protected BucketAggregationBase(IDictionary<string, IAggregation> aggregations) : base(aggregations) { }
	}

	public abstract class BucketAggregationBaseDescriptor<TBucketAggregation, T> 
		: IAggregationDescriptor, IBucketAggregator 
		where TBucketAggregation : BucketAggregationBaseDescriptor<TBucketAggregation, T>
		where T : class
	{
		IDictionary<string, IAggregationContainer> IBucketAggregator.Aggregations { get; set; }

		public TBucketAggregation Aggregations(Func<AggregationDescriptor<T>, AggregationDescriptor<T>> selector)
		{	
			var aggs = selector(new AggregationDescriptor<T>());
			if (aggs == null) return (TBucketAggregation)this;
			((IBucketAggregator)this).Aggregations = ((IAggregationContainer)aggs).Aggregations;
			return (TBucketAggregation)this;
		}
	}
}