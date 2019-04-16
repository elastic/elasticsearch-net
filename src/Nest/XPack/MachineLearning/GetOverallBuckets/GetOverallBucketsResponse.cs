using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IGetOverallBucketsResponse : IResponse
	{
		[DataMember(Name ="count")]
		long Count { get; }

		[DataMember(Name ="overall_buckets")]
		IReadOnlyCollection<OverallBucket> OverallBuckets { get; }
	}

	public class GetOverallBucketsResponse : ResponseBase, IGetOverallBucketsResponse
	{
		public long Count { get; internal set; }

		public IReadOnlyCollection<OverallBucket> OverallBuckets { get; internal set; } = EmptyReadOnly<OverallBucket>.Collection;
	}
}
