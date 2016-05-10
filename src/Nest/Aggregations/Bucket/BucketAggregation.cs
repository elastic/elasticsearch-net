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
		: IBucketAggregation, IDescriptor
		where TBucketAggregationDescriptor : BucketAggregationDescriptorBase<TBucketAggregationDescriptor, TBucketAggregationInterface, T>
			, TBucketAggregationInterface, IBucketAggregation
		where T : class
		where TBucketAggregationInterface : class, IBucketAggregation
	{
		AggregationDictionary IBucketAggregation.Aggregations { get; set; }

		protected TBucketAggregationDescriptor Assign(Action<TBucketAggregationInterface> assigner) =>
			Fluent.Assign(((TBucketAggregationDescriptor)this), assigner);

		protected TBucketAggregationInterface Self => (TBucketAggregationDescriptor)this;

		string IAggregation.Name { get; set; }

		IDictionary<string, object> IAggregation.Meta { get; set; }

		public TBucketAggregationDescriptor Aggregations(Func<AggregationContainerDescriptor<T>, IAggregationContainer> selector) =>
			Assign(a => a.Aggregations = selector?.Invoke(new AggregationContainerDescriptor<T>())?.Aggregations);

		public TBucketAggregationDescriptor Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) =>
			Assign(a => a.Meta = selector?.Invoke(new FluentDictionary<string, object>()));
	}

}
