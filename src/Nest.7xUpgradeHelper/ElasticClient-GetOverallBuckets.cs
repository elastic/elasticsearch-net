using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieves machine learning job results for one or more buckets.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetOverallBucketsResponse GetOverallBuckets(this IElasticClient client, Id jobId,
			Func<GetOverallBucketsDescriptor, IGetOverallBucketsRequest> selector = null
		)
			=> client.MachineLearning.GetOverallBuckets(jobId, selector);

		/// <summary>
		/// Retrieves machine learning job results for one or more buckets.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetOverallBucketsResponse GetOverallBuckets(this IElasticClient client, IGetOverallBucketsRequest request)
			=> client.MachineLearning.GetOverallBuckets(request);

		/// <summary>
		/// Retrieves machine learning job results for one or more buckets.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetOverallBucketsResponse> GetOverallBucketsAsync(this IElasticClient client, Id jobId,
			Func<GetOverallBucketsDescriptor, IGetOverallBucketsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetOverallBucketsAsync(jobId, selector, ct);

		/// <summary>
		/// Retrieves machine learning job results for one or more buckets.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetOverallBucketsResponse> GetOverallBucketsAsync(this IElasticClient client, IGetOverallBucketsRequest request,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetOverallBucketsAsync(request, ct);
	}
}
