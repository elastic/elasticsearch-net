
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class FiltersBucketItem : BucketItemBase
	{
		public FiltersBucketItem() { }
		public FiltersBucketItem(IDictionary<string, IAggregationResult> aggregations) : base(aggregations) { }

		public long DocCount { get; set; }
	}

	public class FiltersBucket : Bucket<FiltersBucketItem>
	{
		public FiltersBucket() { }

		public FiltersBucket(IDictionary<string, IAggregationResult> aggregations) : base(aggregations) { }

		public DocCountBucket NamedBucket(string key) => this.Global(key);

		public IList<FiltersBucketItem> AnonymousBuckets() => this.Items?.OfType<FiltersBucketItem>().ToList();
	}
}
