using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Retrieves machine learning job results for one or more buckets.
		/// </summary>
		GetOverallBucketsResponse GetOverallBuckets(Id jobId, Func<GetOverallBucketsDescriptor, IGetOverallBucketsRequest> selector = null);

		/// <summary>
		/// Retrieves machine learning job results for one or more buckets.
		/// </summary>
		GetOverallBucketsResponse GetOverallBuckets(IGetOverallBucketsRequest request);

		/// <summary>
		/// Retrieves machine learning job results for one or more buckets.
		/// </summary>
		Task<GetOverallBucketsResponse> GetOverallBucketsAsync(Id jobId,
			Func<GetOverallBucketsDescriptor, IGetOverallBucketsRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <summary>
		/// Retrieves machine learning job results for one or more buckets.
		/// </summary>
		Task<GetOverallBucketsResponse> GetOverallBucketsAsync(IGetOverallBucketsRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetOverallBucketsResponse GetOverallBuckets(Id jobId, Func<GetOverallBucketsDescriptor, IGetOverallBucketsRequest> selector = null) =>
			GetOverallBuckets(selector.InvokeOrDefault(new GetOverallBucketsDescriptor(jobId)));

		/// <inheritdoc />
		public GetOverallBucketsResponse GetOverallBuckets(IGetOverallBucketsRequest request) =>
			DoRequest<IGetOverallBucketsRequest, GetOverallBucketsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetOverallBucketsResponse> GetOverallBucketsAsync(
			Id jobId,
			Func<GetOverallBucketsDescriptor, IGetOverallBucketsRequest> selector = null,
			CancellationToken cancellationToken = default
		) => GetOverallBucketsAsync(selector.InvokeOrDefault(new GetOverallBucketsDescriptor(jobId)), cancellationToken);

		/// <inheritdoc />
		public Task<GetOverallBucketsResponse> GetOverallBucketsAsync(IGetOverallBucketsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetOverallBucketsRequest, GetOverallBucketsResponse, GetOverallBucketsResponse>
				(request, request.RequestParameters, ct);
	}
}
