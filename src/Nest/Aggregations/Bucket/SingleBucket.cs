using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class SingleBucket : BucketBase
	{
		public SingleBucket() { }

		public SingleBucket(IDictionary<string, IAggregationResult> aggregations) : base(aggregations) { }

		public long DocCount { get; set; }
	}
}
