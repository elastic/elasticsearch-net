using System;
using System.Collections.Generic;

namespace Nest
{
	public interface IBucketItem { }

	public interface IBucket : IAggregationResult
	{
		IDictionary<string, IAggregationResult> Aggregations { get; }
	}

	public abstract class BucketBase : AggregationsHelper, IBucket
	{
		protected BucketBase() { }
		protected BucketBase(IDictionary<string, IAggregationResult> aggregations) : base(aggregations) { }

		public IDictionary<string, object> Meta { get; set; }
	}

	public class Bucket<TBucketItem> : BucketBase
		where TBucketItem : IBucketItem
	{
		public Bucket() { }
		public Bucket(IDictionary<string, IAggregationResult> aggregations) : base(aggregations) { }

		public IList<TBucketItem> Items { get; set; }
	}

	// Intermediate object used for deserialization
	internal class Bucket : IAggregationResult
	{
		public IEnumerable<IAggregationResult> Items { get; set; }
		public long? DocCountErrorUpperBound { get; set; }
		public long? SumOtherDocCount { get; set; }
		public IDictionary<string, object> Meta { get; set; }
		public long DocCount { get; set; }
	}
}
