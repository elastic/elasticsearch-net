using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Nest
{

	[JsonObject(MemberSerialization.OptIn)]
	public abstract class BucketAggregateBase : AggregationsHelper, IAggregate
	{
		public IDictionary<string, object> Meta { get; set; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class MultiBucketAggregate<TBucket> : BucketAggregateBase
		where TBucket : IBucket
	{
		[JsonProperty("buckets")]
		public List<TBucket> Buckets { get; set; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SingleBucketAggregate : BucketAggregateBase
	{
		[JsonProperty("doc_count")]
		public long DocCount { get; internal set; }
	}
}
