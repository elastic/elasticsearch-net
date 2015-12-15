using System.Collections.Generic;

namespace Nest
{
	public interface IBucket : IAggregation
	{
		IDictionary<string, IAggregation> Aggregations { get; }
	}

	public interface IBucketItem : IAggregation
	{
	}

	public class Bucket : IAggregation
	{
		public IEnumerable<IAggregation> Items { get; set; }
		public long? DocCountErrorUpperBound { get; set; }
		public long? SumOtherDocCount { get; set; }
	}

	public class Bucket<TBucketItem> : BucketBase
		where TBucketItem : IBucketItem
	{
		public Bucket() { }
		public Bucket(IDictionary<string, IAggregation> aggregations) : base(aggregations) { }

		public IList<TBucketItem> Items { get; set; }
	}

	public abstract class BucketBase : AggregationsHelper, IBucket
	{
		protected BucketBase() { }
		protected BucketBase(IDictionary<string, IAggregation> aggregations) : base(aggregations) { }
	}
}
