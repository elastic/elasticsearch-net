using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IGetInfluencersResponse : IResponse
	{
		[DataMember(Name ="count")]
		long Count { get; }

		[DataMember(Name ="influencers")]
		IReadOnlyCollection<BucketInfluencer> Influencers { get; }
	}

	public class GetInfluencersResponse : ResponseBase, IGetInfluencersResponse
	{
		public long Count { get; internal set; }

		public IReadOnlyCollection<BucketInfluencer> Influencers { get; internal set; } = EmptyReadOnly<BucketInfluencer>.Collection;
	}
}
