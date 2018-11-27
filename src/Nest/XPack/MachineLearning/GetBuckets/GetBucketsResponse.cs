using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IGetBucketsResponse : IResponse
	{
		[DataMember(Name ="buckets")]
		IReadOnlyCollection<Bucket> Buckets { get; }

		[DataMember(Name ="count")]
		long Count { get; }
	}

	public class GetBucketsResponse : ResponseBase, IGetBucketsResponse
	{
		public IReadOnlyCollection<Bucket> Buckets { get; internal set; } = EmptyReadOnly<Bucket>.Collection;
		public long Count { get; internal set; }
	}
}
