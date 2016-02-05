
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class FiltersBucketItem : BucketBase
	{
		public FiltersBucketItem() { }
		public FiltersBucketItem(IDictionary<string, IAggregate> aggregations) : base(aggregations) { }

		public long DocCount { get; set; }
	}

	public class FiltersAggregate : MultiBucketAggregate<FiltersBucketItem>
	{
		public FiltersAggregate() { }

		public FiltersAggregate(IDictionary<string, IAggregate> aggregations) : base(aggregations) { }

		public SingleBucketAggregate NamedBucket(string key) => this.Global(key);

		public IList<FiltersBucketItem> AnonymousBuckets() => this.Buckets?.OfType<FiltersBucketItem>().ToList();
	}
}
