using System;
using System.Collections.Generic;

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

	public abstract class BucketAggregationDescriptorBase<TBucketAggregationDescriptor, TBucketAggregationInterface, T>
		: AggregationDescriptorBase<TBucketAggregationDescriptor, TBucketAggregationInterface, T>, IBucketAggregation
		where TBucketAggregationDescriptor : BucketAggregationDescriptorBase<TBucketAggregationDescriptor, TBucketAggregationInterface, T>
			, TBucketAggregationInterface, IBucketAggregation
		where TBucketAggregationInterface : class, IBucketAggregation
		where T : class
	{
		AggregationDictionary IBucketAggregation.Aggregations { get; set; }

		public TBucketAggregationDescriptor Aggregations(Func<AggregationContainerDescriptor<T>, IAggregationContainer> selector) =>
			Assign(a => a.Aggregations = selector?.Invoke(new AggregationContainerDescriptor<T>())?.Aggregations);
	}
}
