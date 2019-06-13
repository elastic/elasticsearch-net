using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieves machine learning job results for one or more buckets.
		/// </summary>
		public static GetOverallBucketsResponse GetOverallBuckets(this IElasticClient client,Id jobId, Func<GetOverallBucketsDescriptor, IGetOverallBucketsRequest> selector = null);

		/// <summary>
		/// Retrieves machine learning job results for one or more buckets.
		/// </summary>
		public static GetOverallBucketsResponse GetOverallBuckets(this IElasticClient client,IGetOverallBucketsRequest request);

		/// <summary>
		/// Retrieves machine learning job results for one or more buckets.
		/// </summary>
		public static Task<GetOverallBucketsResponse> GetOverallBucketsAsync(this IElasticClient client,Id jobId,
			Func<GetOverallBucketsDescriptor, IGetOverallBucketsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <summary>
		/// Retrieves machine learning job results for one or more buckets.
		/// </summary>
		public static Task<GetOverallBucketsResponse> GetOverallBucketsAsync(this IElasticClient client,IGetOverallBucketsRequest request,
			CancellationToken ct = default
		);
	}

}
