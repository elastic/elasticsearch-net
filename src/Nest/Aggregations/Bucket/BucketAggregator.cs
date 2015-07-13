using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{

	public interface IBucketAggregator : IAggregator
	{
		AggregationDictionary Aggregations { get; set; }
	}

	public abstract class BucketAggregator : IBucketAggregator
	{
		AggregationDictionary IBucketAggregator.Aggregations { get; set; }
	}

	public abstract class BucketAgg : AggregatorBase, IBucketAggregator
	{
		public AggregationDictionary Aggregations { get; set; }

		protected BucketAgg(string name) : base(name) { }
	}

	public abstract class BucketAggregatorBaseDescriptor<TBucketAggregation, TBucketAggregationInterface, T>
		: IBucketAggregator
		where TBucketAggregation : BucketAggregatorBaseDescriptor<TBucketAggregation, TBucketAggregationInterface, T>
			, TBucketAggregationInterface, IBucketAggregator
		where T : class
		where TBucketAggregationInterface : class, IBucketAggregator
	{
		AggregationDictionary IBucketAggregator.Aggregations { get; set; }
		
		protected TBucketAggregation Assign(Action<TBucketAggregationInterface> assigner) =>
			Fluent.Assign(((TBucketAggregation)this), assigner);

		protected TBucketAggregationInterface Self => (TBucketAggregation)this;

		public TBucketAggregation Aggregations(Func<AggregationContainerDescriptor<T>, IAggregationContainer> selector) =>
			Assign(a => a.Aggregations = selector?.Invoke(new AggregationContainerDescriptor<T>())?.Aggregations);
	}

}