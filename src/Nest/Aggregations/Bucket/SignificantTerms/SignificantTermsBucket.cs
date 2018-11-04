using System.Collections.Generic;

namespace Nest
{
	public class SignificantTermsBucket : BucketBase, IBucket
	{
		public SignificantTermsBucket() { }

		public SignificantTermsBucket(IDictionary<string, IAggregate> aggregations) : base(aggregations) { }

		public long BgCount { get; set; }
		public long DocCount { get; set; }

		public string Key { get; set; }
		public double Score { get; set; }
	}
}
