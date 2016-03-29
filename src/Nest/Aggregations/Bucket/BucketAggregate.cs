using System;
using System.Collections.Generic;

namespace Nest
{
	public abstract class BucketAggregateBase : AggregationsHelper, IAggregate
	{
		protected BucketAggregateBase() { }
		protected BucketAggregateBase(IDictionary<string, IAggregate> aggregations) : base(aggregations) { }

		public IDictionary<string, object> Meta { get; set; }
	}

	public class MultiBucketAggregate<TBucket> : BucketAggregateBase
		where TBucket : IBucket
	{
		public MultiBucketAggregate() { }
		public MultiBucketAggregate(IDictionary<string, IAggregate> aggregations) : base(aggregations) { }

		public IList<TBucket> Buckets { get; set; }
	}

	public class SingleBucketAggregate : BucketAggregateBase
	{
		public SingleBucketAggregate() { }
		public SingleBucketAggregate(IDictionary<string, IAggregate> aggregations) : base(aggregations) { }

		public long DocCount { get; internal set; }
	}

	// Intermediate object used for deserialization
	public class BucketAggregate : IAggregate
	{
		public IEnumerable<IBucket> Items { get; set; }
		public long? DocCountErrorUpperBound { get; set; }
		public long? SumOtherDocCount { get; set; }
		public IDictionary<string, object> Meta { get; set; }
		public long DocCount { get; set; }
	}
}
