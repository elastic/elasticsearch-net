using System.Collections.Generic;

namespace Nest
{
	public class SignificantTermsBucket : BucketBase, IBucket
	{
		public SignificantTermsBucket(IReadOnlyDictionary<string, IAggregate> dict) : base(dict) { }

		public long BgCount { get; set; }
		public long DocCount { get; set; }

		public string Key { get; set; }
		public double Score { get; set; }
	}
}
