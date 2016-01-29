using System.Collections.Generic;

namespace Nest
{
	public interface IDocCountBucket : IBucket
	{
		long DocCount { get; }
	}

	public class DocCountBucket<TBucketItem> : BucketBase, IDocCountBucket
		where TBucketItem : IBucketItem
	{
		public DocCountBucket() { }
		public DocCountBucket(IDictionary<string, IAggregationResult> aggregations) : base(aggregations) { }

		public IList<TBucketItem> Items { get; set; }

		public long DocCount { get; internal set; }
	}

	public class DocCountBucket : BucketBase, IAggregationResult
	{
		public long DocCount { get; set; }
		public IEnumerable<IAggregationResult> Items { get; set; }

	}
}
