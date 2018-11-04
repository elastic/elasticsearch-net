using System.Collections.Generic;

namespace Nest
{
	public class KeyedBucket<TKey> : BucketBase
	{
		public KeyedBucket() { }

		public KeyedBucket(IDictionary<string, IAggregate> aggregations) : base(aggregations) { }

		public long? DocCount { get; set; }

		public long? DocCountErrorUpperBound { get; set; }

		public TKey Key { get; set; }
		public string KeyAsString { get; set; }
	}
}
