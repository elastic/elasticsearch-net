using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{

	public interface IBucketAggregator
	{
		IDictionary<string, IAggregationContainer> Aggregations { get; set; }
	}

	public abstract class BucketAggregator : IBucketAggregator
	{
		public IDictionary<string, IAggregationContainer> Aggregations { get; set; }
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