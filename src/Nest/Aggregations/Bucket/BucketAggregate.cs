using System.Collections.Generic;

namespace Nest
{
	public abstract class BucketAggregateBase : AggregationsHelper, IAggregate
	{
		protected BucketAggregateBase() { }

		protected BucketAggregateBase(IDictionary<string, IAggregate> aggregations) : base(aggregations) { }

		public IReadOnlyDictionary<string, object> Meta { get; set; } = EmptyReadOnly<string, object>.Dictionary;
	}

	public class MultiBucketAggregate<TBucket> : BucketAggregateBase
		where TBucket : IBucket
	{
		public MultiBucketAggregate() { }

		public MultiBucketAggregate(IDictionary<string, IAggregate> aggregations) : base(aggregations) { }

		public IReadOnlyCollection<TBucket> Buckets { get; set; } = EmptyReadOnly<TBucket>.Collection;
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
		//TODO non nullable in 6.0, introduced in 5.5
		public long? BgCount { get; set; }
		public long DocCount { get; set; }
		public long? DocCountErrorUpperBound { get; set; }
		public IReadOnlyCollection<IBucket> Items { get; set; } = EmptyReadOnly<IBucket>.Collection;
		public IReadOnlyDictionary<string, object> Meta { get; set; } = EmptyReadOnly<string, object>.Dictionary;
		public long? SumOtherDocCount { get; set; }
	}
}
