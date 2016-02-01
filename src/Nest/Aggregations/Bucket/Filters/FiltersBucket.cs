
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class FiltersBucket : Bucket, IAggregationItem
	{
		public FiltersBucket(IEnumerable<IAggregationItem> items)
		{
			Items = items;
		}

		public FiltersBucket(IDictionary<string, IAggregationItem> aggregations) : base(aggregations) { }

		public DocCountBucket NamedBucket(string key) => this.Global(key);

		public IList<DocCountBucket> AnonymousBuckets() => this.Items?.OfType<DocCountBucket>().ToList();

		public IEnumerable<IAggregationItem> Items { get; set; }
	}
}
