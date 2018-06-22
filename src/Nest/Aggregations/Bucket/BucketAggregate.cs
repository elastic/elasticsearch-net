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

		public long DocCount { get; internal set; }
	}

	public class MultiBucketAggregate<TBucket> : IAggregate
		where TBucket : IBucket
	{
		public IReadOnlyDictionary<string, object> Meta { get; set; }

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
		public long BgCount { get; set; }
	}
}
