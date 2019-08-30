using System.Collections.Generic;

namespace Nest
{
	public class RareTermsBucket<TKey> : BucketBase
	{
		public RareTermsBucket(IReadOnlyDictionary<string, IAggregate> dict) : base(dict) { }

		public long DocCount { get; set; }

		public TKey Key { get; set; }
	}
}
