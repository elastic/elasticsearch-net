using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.GetBuckets(), please update this usage.")]
		public static GetBucketsResponse GetBuckets(this IElasticClient client, Id jobId,
			Func<GetBucketsDescriptor, IGetBucketsRequest> selector = null
		)
			=> client.MachineLearning.GetBuckets(jobId, selector);

		[Obsolete("Moved to client.MachineLearning.GetBuckets(), please update this usage.")]
		public static GetBucketsResponse GetBuckets(this IElasticClient client, IGetBucketsRequest request)
			=> client.MachineLearning.GetBuckets(request);

		[Obsolete("Moved to client.MachineLearning.GetBucketsAsync(), please update this usage.")]
		public static Task<GetBucketsResponse> GetBucketsAsync(this IElasticClient client, Id jobId,
			Func<GetBucketsDescriptor, IGetBucketsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetBucketsAsync(jobId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.GetBucketsAsync(), please update this usage.")]
		public static Task<GetBucketsResponse> GetBucketsAsync(this IElasticClient client, IGetBucketsRequest request, CancellationToken ct = default)
			=> client.MachineLearning.GetBucketsAsync(request, ct);
	}
}
