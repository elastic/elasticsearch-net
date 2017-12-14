
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class FiltersBucketItem : BucketBase
	{
		public FiltersBucketItem(IReadOnlyDictionary<string, IAggregate> dict) : base(dict) { }

		public long DocCount { get; set; }
	}

	public class FiltersAggregate : MultiBucketAggregate<FiltersBucketItem>
	{
		public SingleBucketAggregate NamedBucket(string key) => this.Global(key);

		public IList<FiltersBucketItem> AnonymousBuckets() => this.Buckets?.ToList();
	}
}
