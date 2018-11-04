using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetInfluencersResponse : IResponse
	{
		[JsonProperty("count")]
		long Count { get; }

		[JsonProperty("influencers")]
		IReadOnlyCollection<BucketInfluencer> Influencers { get; }
	}

	public class GetInfluencersResponse : ResponseBase, IGetInfluencersResponse
	{
		public long Count { get; internal set; }

		public IReadOnlyCollection<BucketInfluencer> Influencers { get; internal set; } = EmptyReadOnly<BucketInfluencer>.Collection;
	}
}
