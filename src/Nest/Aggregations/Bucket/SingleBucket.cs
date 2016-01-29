using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class Bucket : BucketBase
	{
		public Bucket() { }

		public Bucket(IDictionary<string, IAggregationResult> aggregations) : base(aggregations) { }

		public long DocCount { get; set; }
	}
}
