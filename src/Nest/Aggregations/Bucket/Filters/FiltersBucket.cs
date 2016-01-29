
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class FiltersBucket : BucketBase, IAggregationResult
	{
		public FiltersBucket(IEnumerable<IAggregationResult> items)
		{
			Items = items;
		}

		public FiltersBucket(IDictionary<string, IAggregationResult> aggregations) : base(aggregations) { }

		public SingleBucket NamedBucket(string key) => this.Global(key);

		public IList<SingleBucket> AnonymousBuckets() => this.Items?.OfType<SingleBucket>().ToList();

		public IEnumerable<IAggregationResult> Items { get; set; }
	}
}
