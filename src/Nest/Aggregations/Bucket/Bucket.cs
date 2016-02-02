using System;
using System.Collections.Generic;

namespace Nest
{

	public class Bucket : AggregationsHelper, IAggregationResult
	{
		public Bucket() { }
		public Bucket(IDictionary<string, IAggregationResult> aggregations) : base(aggregations) { }

		public IDictionary<string, object> Meta { get; set; }
	}

	public class Bucket<TBucketItem> : Bucket
		where TBucketItem : IBucketItem
	{
		public Bucket() { }
		public Bucket(IDictionary<string, IAggregationResult> aggregations) : base(aggregations) { }

		public IList<TBucketItem> Items { get; set; }
	}

	public class DocCountBucket : Bucket
	{
		public DocCountBucket() { }
		public DocCountBucket(IDictionary<string, IAggregationResult> aggregations) : base(aggregations) { }

		public long DocCount { get; internal set; }
	}

	public class DocCountBucket<TBucketItem> : DocCountBucket
		where TBucketItem : IBucketItem
	{
		public DocCountBucket() { }
		public DocCountBucket(IDictionary<string, IAggregationResult> aggregations) : base(aggregations) { }

		public IList<TBucketItem> Items { get; set; }
	}

	// Intermediate object used for deserialization
	internal class BucketDto : IAggregationResult
	{
		public IEnumerable<IBucketItem> Items { get; set; }
		public long? DocCountErrorUpperBound { get; set; }
		public long? SumOtherDocCount { get; set; }
		public IDictionary<string, object> Meta { get; set; }
		public long DocCount { get; set; }
	}
}
