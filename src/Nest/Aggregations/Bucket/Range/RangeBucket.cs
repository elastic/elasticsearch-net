using System.Collections.Generic;

namespace Nest
{
	public class RangeBucket : BucketBase
	{
		public RangeBucket(IReadOnlyDictionary<string, IAggregate> dict) : base(dict) { }

		public long DocCount { get; set; }
		public double? From { get; set; }
		public string FromAsString { get; set; }

		public string Key { get; set; }
		public double? To { get; set; }
		public string ToAsString { get; set; }
	}
}
