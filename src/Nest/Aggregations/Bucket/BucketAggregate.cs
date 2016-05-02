using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Nest
{
	public abstract class BucketAggregateBase : AggregationsHelper, IAggregate
	{
		public IDictionary<string, object> Meta { get; set; }
	}

	public class MultiBucketAggregate<TBucket> : BucketAggregateBase
		where TBucket : IBucket
	{
		[JsonProperty("buckets")]
		public IList<TBucket> Buckets { get; internal set; }
	}

	public class SingleBucketAggregate : BucketAggregateBase
	{
		[JsonProperty("doc_count")]
		public long DocCount { get; internal set; }
	}
}
