using System.Collections.Generic;

namespace Nest
{
	public interface IBucket : IAggregationResult
	{
		IDictionary<string, IAggregationResult> Aggregations { get; }
	}

	public interface IBucketItem : IAggregationResult
	{
	}

	public class Bucket : IAggregationResult
	{
		public IEnumerable<IAggregationResult> Items { get; set; }
		public long? DocCountErrorUpperBound { get; set; }
		public long? SumOtherDocCount { get; set; }
	}

	public class Bucket<TBucketItem> : BucketBase
		where TBucketItem : IBucketItem
	{
		public Bucket() { }
		public Bucket(IDictionary<string, IAggregationResult> aggregations) : base(aggregations) { }

		public IList<TBucketItem> Items { get; set; }
	}

	public abstract class BucketBase : AggregationsHelper, IBucket
	{
		protected BucketBase() { }
		protected BucketBase(IDictionary<string, IAggregationResult> aggregations) : base(aggregations) { }
	}
}
