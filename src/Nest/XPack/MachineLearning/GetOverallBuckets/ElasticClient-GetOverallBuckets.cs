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
		IGetOverallBucketsResponse GetOverallBuckets(Id jobId, Func<GetOverallBucketsDescriptor, IGetOverallBucketsRequest> selector = null);

		/// <summary>
		/// Retrieves machine learning job results for one or more buckets.
		/// </summary>
		IGetOverallBucketsResponse GetOverallBuckets(IGetOverallBucketsRequest request);

		/// <summary>
		/// Retrieves machine learning job results for one or more buckets.
		/// </summary>
		Task<IGetOverallBucketsResponse> GetOverallBucketsAsync(Id jobId,
			Func<GetOverallBucketsDescriptor, IGetOverallBucketsRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <summary>
		/// Retrieves machine learning job results for one or more buckets.
		/// </summary>
		Task<IGetOverallBucketsResponse> GetOverallBucketsAsync(IGetOverallBucketsRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetOverallBucketsResponse GetOverallBuckets(Id jobId, Func<GetOverallBucketsDescriptor, IGetOverallBucketsRequest> selector = null) =>
			GetOverallBuckets(selector.InvokeOrDefault(new GetOverallBucketsDescriptor(jobId)));

		/// <inheritdoc />
		public IGetOverallBucketsResponse GetOverallBuckets(IGetOverallBucketsRequest request) =>
			Dispatch2<IGetOverallBucketsRequest, GetOverallBucketsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IGetOverallBucketsResponse> GetOverallBucketsAsync(
			Id jobId,
			Func<GetOverallBucketsDescriptor, IGetOverallBucketsRequest> selector = null,
			CancellationToken cancellationToken = default
		) => GetOverallBucketsAsync(selector.InvokeOrDefault(new GetOverallBucketsDescriptor(jobId)), cancellationToken);

		/// <inheritdoc />
		public Task<IGetOverallBucketsResponse> GetOverallBucketsAsync(IGetOverallBucketsRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IGetOverallBucketsRequest, IGetOverallBucketsResponse, GetOverallBucketsResponse>
				(request, request.RequestParameters, ct);
	}
}
