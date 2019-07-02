using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{

	public class GetInfluencersResponse : ResponseBase
	{
		[DataMember(Name ="count")]
		public long Count { get; internal set; }

		[DataMember(Name ="influencers")]
		public IReadOnlyCollection<BucketInfluencer> Influencers { get; internal set; } = EmptyReadOnly<BucketInfluencer>.Collection;
	}
}
