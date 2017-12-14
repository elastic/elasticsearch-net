using System.Collections.Generic;

namespace Nest
{
	public class RangeBucket : BucketBase, IBucket
	{
		public RangeBucket(IReadOnlyDictionary<string, IAggregate> dict) : base(dict) { }

		public string Key { get; set; }
		public double? From { get; set; }
		public string FromAsString { get; set; }
		public double? To { get; set; }
		public string ToAsString { get; set; }
		public long DocCount { get; set; }
	}
}
