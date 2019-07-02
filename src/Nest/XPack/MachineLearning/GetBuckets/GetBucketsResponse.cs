using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class GetBucketsResponse : ResponseBase
	{
		[DataMember(Name ="buckets")]
		public IReadOnlyCollection<Bucket> Buckets { get; internal set; } = EmptyReadOnly<Bucket>.Collection;

		[DataMember(Name ="count")]
		public long Count { get; internal set; }
	}
}
