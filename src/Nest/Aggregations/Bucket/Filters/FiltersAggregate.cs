// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class FiltersBucketItem : BucketBase
	{
		public FiltersBucketItem(IReadOnlyDictionary<string, IAggregate> dict) : base(dict) { }

		public long DocCount { get; set; }
	}

	//TODO this is mapped rather oddly we always deserialize as if this is
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

		// Don't sanitize the keys as these are the keys for named buckets
		protected override string Sanitize(string key) => key;

		public IReadOnlyCollection<FiltersBucketItem> Buckets { get; set; } = EmptyReadOnly<FiltersBucketItem>.Collection;

		public SingleBucketAggregate NamedBucket(string key) => Global(key);

		public IList<FiltersBucketItem> AnonymousBuckets() => Buckets?.ToList();
	}
}
