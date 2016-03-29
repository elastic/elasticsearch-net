using System.Collections.Generic;

namespace Nest
{
	public class RangeBucket : BucketBase, IBucket
	{
		public RangeBucket() { }
		public RangeBucket(IDictionary<string, IAggregate> aggregations) : base(aggregations) { }

		public string Key { get; set; }
		public double? From { get; set; }
		public string FromAsString { get; set; }
		public double? To { get; set; }
		public string ToAsString { get; set; }
		public long DocCount { get; set; }
	}
}