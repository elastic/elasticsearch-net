using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public class SingleBucket : BucketBase
	{
		public SingleBucket() { }

		public SingleBucket(IDictionary<string, IAggregationResult> aggregations) : base(aggregations) { }

		public long DocCount { get; set; }
	}
}
