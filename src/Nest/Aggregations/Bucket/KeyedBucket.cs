using System.Collections.Generic;

namespace Nest
{
	public class KeyedBucket<TKey> : BucketBase
	{
		public KeyedBucket(IReadOnlyDictionary<string, IAggregate> dict) : base(dict) { }

		public long? DocCount { get; set; }

		public long? DocCountErrorUpperBound { get; set; }

		public TKey Key { get; set; }
		public string KeyAsString { get; set; }
	}
}
