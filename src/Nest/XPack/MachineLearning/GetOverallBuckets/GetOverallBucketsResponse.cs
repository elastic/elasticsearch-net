using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class GetOverallBucketsResponse : ResponseBase
	{
		[DataMember(Name ="count")]
		public long Count { get; internal set; }

		[DataMember(Name ="overall_buckets")]
		public IReadOnlyCollection<OverallBucket> OverallBuckets { get; internal set; } = EmptyReadOnly<OverallBucket>.Collection;
	}
}
