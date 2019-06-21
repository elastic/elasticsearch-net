using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.MachineLearning.GetOverallBuckets(), please update this usage.")]
		public static GetOverallBucketsResponse GetOverallBuckets(this IElasticClient client, Id jobId,
			Func<GetOverallBucketsDescriptor, IGetOverallBucketsRequest> selector = null
		)
			=> client.MachineLearning.GetOverallBuckets(jobId, selector);

		[Obsolete("Moved to client.MachineLearning.GetOverallBuckets(), please update this usage.")]
		public static GetOverallBucketsResponse GetOverallBuckets(this IElasticClient client, IGetOverallBucketsRequest request)
			=> client.MachineLearning.GetOverallBuckets(request);

		[Obsolete("Moved to client.MachineLearning.GetOverallBucketsAsync(), please update this usage.")]
		public static Task<GetOverallBucketsResponse> GetOverallBucketsAsync(this IElasticClient client, Id jobId,
			Func<GetOverallBucketsDescriptor, IGetOverallBucketsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetOverallBucketsAsync(jobId, selector, ct);

		[Obsolete("Moved to client.MachineLearning.GetOverallBucketsAsync(), please update this usage.")]
		public static Task<GetOverallBucketsResponse> GetOverallBucketsAsync(this IElasticClient client, IGetOverallBucketsRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetOverallBucketsAsync(request, ct);
	}
}
