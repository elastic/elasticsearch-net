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
}