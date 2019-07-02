using System.Collections.Generic;

namespace Nest
{
	public class SignificantTermsBucket<TKey> : BucketBase, IBucket
	{
		public SignificantTermsBucket(IReadOnlyDictionary<string, IAggregate> dict) : base(dict) { }

		public long BgCount { get; set; }
		public long DocCount { get; set; }

		public TKey Key { get; set; }
		public double Score { get; set; }
	}
}
