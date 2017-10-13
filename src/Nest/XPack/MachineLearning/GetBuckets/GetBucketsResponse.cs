using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetBucketsResponse : IResponse
	{
		[JsonProperty("count")]
		long Count { get; }

		[JsonProperty("buckets")]
		IReadOnlyCollection<Bucket> Buckets { get; }
	}

	public class GetBucketsResponse : ResponseBase, IGetBucketsResponse
	{
		public long Count { get; internal set; }

		public IReadOnlyCollection<Bucket> Buckets { get; internal set; } = EmptyReadOnly<Bucket>.Collection;
	}
}
