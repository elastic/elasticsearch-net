using System;
using System.Collections.Generic;

namespace Nest
{
	public abstract class BucketAggregationBase : AggregationsHelper , IBucketAggregation
	{
		protected BucketAggregationBase() { }
		protected BucketAggregationBase(IDictionary<string, IAggregation> aggregations) : base(aggregations) { }
	}

	public abstract class BucketAggregationBaseDescriptor<TBucketAggregation, TI, T>
		: IAggregationDescriptor, IBucketAggregator
		where TBucketAggregation : BucketAggregationBaseDescriptor<TBucketAggregation, TI, T>, TI, IBucketAggregator
		where T : class
		where TI : class, IBucketAggregator
	{
		IDictionary<string, IAggregationContainer> IBucketAggregator.Aggregations { get; set; }

		protected TBucketAggregation Assign(Action<TI> assigner) => Fluent.Assign(((TBucketAggregation)this), assigner);

		protected TI Self => (TI)(TBucketAggregation)this;

		public TBucketAggregation Aggregations(Func<AggregationDescriptor<T>, IAggregationContainer> selector) =>
			Assign(a => a.Aggregations = selector?.Invoke(new AggregationDescriptor<T>())?.Aggregations.NullIfNoKeys());
	}
}