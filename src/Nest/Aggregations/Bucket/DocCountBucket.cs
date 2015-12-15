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
		public DocCountBucket(IDictionary<string, IAggregation> aggregations) : base(aggregations) { }

		public IList<TBucketItem> Items { get; set; }

		public long DocCount { get; internal set; }
	}

	public class DocCountBucket : IAggregation
	{
		public long DocCount { get; set; }
		public IEnumerable<IAggregation> Items { get; set; }
	}
}
