
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class FiltersBucket : IAggregation
	{
		public FiltersBucket(IEnumerable<IAggregation> items)
		{
			Items = items;
		}

		public FiltersBucket(AggregationsHelper helper)
		{
			Aggregations = helper;
		}

		public SingleBucket NamedBucket(string key) => this.Aggregations?.Global(key);

		public IList<SingleBucket> AnonymousBuckets() => this.Items?.OfType<SingleBucket>().ToList();

		private AggregationsHelper Aggregations { get; set; }

		private IEnumerable<IAggregation> Items { get; set; }
	}
}
