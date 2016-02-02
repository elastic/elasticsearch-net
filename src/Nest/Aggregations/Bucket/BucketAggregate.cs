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

	public class MultiBucketAggregate<TBucketItem> : BucketAggregateBase
		where TBucketItem : IBucketItem
	{
		public MultiBucketAggregate() { }
		public MultiBucketAggregate(IDictionary<string, IAggregate> aggregations) : base(aggregations) { }

		public IList<TBucketItem> Buckets { get; set; }
	}

	public class SingleBucketAggregate : BucketAggregateBase
	{
		public SingleBucketAggregate() { }
		public SingleBucketAggregate(IDictionary<string, IAggregate> aggregations) : base(aggregations) { }

		public long DocCount { get; internal set; }
	}

	// Intermediate object used for deserialization
	// TODO we should get rid of this and refactor AggregateJsonConverter
	internal class BucketAggregateData : IAggregate
	{
		public IEnumerable<IBucketItem> Items { get; set; }
		public long? DocCountErrorUpperBound { get; set; }
		public long? SumOtherDocCount { get; set; }
		public IDictionary<string, object> Meta { get; set; }
		public long DocCount { get; set; }
	}
}
