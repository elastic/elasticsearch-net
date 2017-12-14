using System;
using System.Collections.Generic;

namespace Nest
{
	public abstract class BucketAggregateBase : AggregateDictionary , IAggregate
	{
		protected BucketAggregateBase(IReadOnlyDictionary<string, IAggregate> aggregations) : base(aggregations) { }

		public IReadOnlyDictionary<string, object> Meta { get; set; } = EmptyReadOnly<string, object>.Dictionary;
	}

	public class SingleBucketAggregate : BucketAggregateBase
	{
		public SingleBucketAggregate(IReadOnlyDictionary<string, IAggregate> aggregations) : base(aggregations) { }

		public AggregateDictionary Aggregations { get; protected internal set; }

		public long DocCount { get; internal set; }
	}

	public class MultiBucketAggregate<TBucket> : BucketAggregateBase
		where TBucket : IBucket
	{
		public MultiBucketAggregate() : base(EmptyReadOnly<string, IAggregate>.Dictionary) { }

		public IReadOnlyCollection<TBucket> Buckets { get; set; } = EmptyReadOnly<TBucket>.Collection;
	}


	// Intermediate object used for deserialization
	public class BucketAggregate : IAggregate
	{
		public IReadOnlyCollection<IBucket> Items { get; set; } = EmptyReadOnly<IBucket>.Collection;
		public long? DocCountErrorUpperBound { get; set; }
		public long? SumOtherDocCount { get; set; }
		public IReadOnlyDictionary<string, object> Meta { get; set; } = EmptyReadOnly<string, object>.Dictionary;
		public long DocCount { get; set; }
		//TODO non nullable in 6.0, introduced in 5.5
		public long? BgCount { get; set; }
	}
}
