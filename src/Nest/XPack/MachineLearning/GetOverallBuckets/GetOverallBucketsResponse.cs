using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetOverallBucketsResponse : IResponse
	{
		[JsonProperty("count")]
		long Count { get; }

		[JsonProperty("overall_buckets")]
		IReadOnlyCollection<OverallBucket> OverallBuckets { get; }
	}

	public class GetOverallBucketsResponse : ResponseBase, IGetOverallBucketsResponse
	{
		public long Count { get; internal set; }

		public IReadOnlyCollection<OverallBucket> OverallBuckets { get; internal set; } = EmptyReadOnly<OverallBucket>.Collection;
	}
}
