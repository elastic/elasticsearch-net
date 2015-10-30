using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
	public class SingleBucket : BucketBase
	{
		public SingleBucket() { }

		public SingleBucket(IDictionary<string, IAggregation> aggregations) : base(aggregations) { }

		[JsonProperty("doc_count")]
		public long DocCount { get; set; }
	}
}
