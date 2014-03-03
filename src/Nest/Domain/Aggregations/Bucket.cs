using System.Collections.Generic;

namespace Nest
{
	public class Bucket<TBucketItem> : BucketAggregationBase
		where TBucketItem : IBucketItem
	{
		public IList<TBucketItem> Items { get; set; }
	}
	public class Bucket : IAggregation
	{
		public IEnumerable<IAggregation> Items { get; set; }
	}
}