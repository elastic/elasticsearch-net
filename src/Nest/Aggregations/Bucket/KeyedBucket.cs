using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nest
{
	public class KeyedBucket<TKey> : BucketBase
	{
		public KeyedBucket(IReadOnlyDictionary<string, IAggregate> dict) : base(dict) { }

		public TKey Key { get; set; }
		public string KeyAsString { get; set; }
		public long? DocCount { get; set; }

		public long? DocCountErrorUpperBound { get; set; }
	}
}
