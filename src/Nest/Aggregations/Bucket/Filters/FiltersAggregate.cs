
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class FiltersBucketItem : BucketBase
	{
		public FiltersBucketItem(IReadOnlyDictionary<string, IAggregate> dict) : base(dict) { }

		public long DocCount { get; set; }
	}

	//TODO this is mapped rather odly we always deserialize as if this is
	// {
	//    "agg1" : { ...},
	//	  "agg2" : { ... }
	//}
	// while its actually a buckets response
	// {
	//   "buckets" : {} || []
	//}
	// where buckets is either an array or object. We fix this in the helper where we
	// move the aggs from itself into a *new* filters aggregate using the parameterless constructor
	public class FiltersAggregate : BucketAggregateBase
	{
		public FiltersAggregate() : base(EmptyReadOnly<string, IAggregate>.Dictionary) { }
		public FiltersAggregate(IReadOnlyDictionary<string, IAggregate> aggregations) : base(aggregations) { }

		public SingleBucketAggregate NamedBucket(string key) => this.Global(key);

		public IList<FiltersBucketItem> AnonymousBuckets() => this.Buckets?.ToList();

		public IReadOnlyCollection<FiltersBucketItem> Buckets { get; set; } = EmptyReadOnly<FiltersBucketItem>.Collection;

	}
}
