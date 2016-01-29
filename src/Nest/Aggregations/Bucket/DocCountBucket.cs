using System.Collections.Generic;

namespace Nest
{
	public interface IDocCountBucket : IBucket
	{
		long DocCount { get; }
	}

	public class DocCountBucket<TBucketItem> : Bucket<TBucketItem>, IDocCountBucket
		where TBucketItem : IBucketItem
	{
		public DocCountBucket() { }
		public DocCountBucket(IDictionary<string, IAggregationResult> aggregations) : base(aggregations) { }

		public long DocCount { get; internal set; }
	}
}
