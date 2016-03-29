using System;
using System.Collections.Generic;

namespace Nest
{
	public class HistogramBucket : BucketBase, IBucket
	{
		public HistogramBucket() { }
		public HistogramBucket(IDictionary<string, IAggregate> aggregations) : base(aggregations) { }

		public long Key { get; set; }
		public string KeyAsString { get; set; }
		public long DocCount { get; set; }
	}
}