
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class FiltersBucket : IAggregationResult
	{
		public FiltersBucket(IEnumerable<IAggregationResult> items)
		{
			Items = items;
		}

		public FiltersBucket(AggregationsHelper helper)
		{
			Aggregations = helper;
		}

		public SingleBucket NamedBucket(string key) => this.Aggregations?.Global(key);

		public IList<SingleBucket> AnonymousBuckets() => this.Items?.OfType<SingleBucket>().ToList();

		public AggregationsHelper Aggregations { get; set; }

		public IEnumerable<IAggregationResult> Items { get; set; }
	}
}
