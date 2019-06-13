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
		public static GetBucketsResponse GetBuckets(this IElasticClient client,Id jobId, Func<GetBucketsDescriptor, IGetBucketsRequest> selector = null);

		/// <inheritdoc />
		public static GetBucketsResponse GetBuckets(this IElasticClient client,IGetBucketsRequest request);

		/// <inheritdoc />
		public static Task<GetBucketsResponse> GetBucketsAsync(this IElasticClient client,Id jobId, Func<GetBucketsDescriptor, IGetBucketsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<GetBucketsResponse> GetBucketsAsync(this IElasticClient client,IGetBucketsRequest request, CancellationToken ct = default);
	}

}
