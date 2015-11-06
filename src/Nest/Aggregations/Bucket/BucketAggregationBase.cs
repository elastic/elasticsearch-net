using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public interface IBucketAggregation : IAggregation
	{
		AggregationDictionary Aggregations { get; set; }
	}

	public abstract class BucketAggregationBase : AggregationBase, IBucketAggregation
	{
		public AggregationDictionary Aggregations { get; set; }

		internal BucketAggregationBase() { }

		protected BucketAggregationBase(string name) : base(name) { }
	}

	public abstract class BucketAggregationDescriptorBase<TBucketAggregation, TBucketAggregationInterface, T>
		: IBucketAggregation
		where TBucketAggregation : BucketAggregationDescriptorBase<TBucketAggregation, TBucketAggregationInterface, T>
			, TBucketAggregationInterface, IBucketAggregation
		where T : class
		where TBucketAggregationInterface : class, IBucketAggregation
	{
		AggregationDictionary IBucketAggregation.Aggregations { get; set; }
		
		protected TBucketAggregation Assign(Action<TBucketAggregationInterface> assigner) =>
			Fluent.Assign(((TBucketAggregation)this), assigner);

		protected TBucketAggregationInterface Self => (TBucketAggregation)this;

		public TBucketAggregation Aggregations(Func<AggregationContainerDescriptor<T>, IAggregationContainer> selector) =>
			Assign(a => a.Aggregations = selector?.Invoke(new AggregationContainerDescriptor<T>())?.Aggregations);
	}

}