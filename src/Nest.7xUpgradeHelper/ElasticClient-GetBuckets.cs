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
public static GetBucketsResponse GetBuckets(this IElasticClient client, Id jobId,
			Func<GetBucketsDescriptor, IGetBucketsRequest> selector = null
		)
			=> client.MachineLearning.GetBuckets(jobId, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetBucketsResponse GetBuckets(this IElasticClient client, IGetBucketsRequest request)
			=> client.MachineLearning.GetBuckets(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetBucketsResponse> GetBucketsAsync(this IElasticClient client, Id jobId,
			Func<GetBucketsDescriptor, IGetBucketsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.MachineLearning.GetBucketsAsync(jobId, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetBucketsResponse> GetBucketsAsync(this IElasticClient client, IGetBucketsRequest request, CancellationToken ct = default)
			=> client.MachineLearning.GetBucketsAsync(request, ct);
	}
}
