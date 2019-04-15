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
		GetBucketsResponse GetBuckets(Id jobId, Func<GetBucketsDescriptor, IGetBucketsRequest> selector = null);

		/// <inheritdoc />
		GetBucketsResponse GetBuckets(IGetBucketsRequest request);

		/// <inheritdoc />
		Task<GetBucketsResponse> GetBucketsAsync(Id jobId, Func<GetBucketsDescriptor, IGetBucketsRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc />
		Task<GetBucketsResponse> GetBucketsAsync(IGetBucketsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetBucketsResponse GetBuckets(Id jobId, Func<GetBucketsDescriptor, IGetBucketsRequest> selector = null) =>
			GetBuckets(selector.InvokeOrDefault(new GetBucketsDescriptor(jobId)));

		/// <inheritdoc />
		public GetBucketsResponse GetBuckets(IGetBucketsRequest request) =>
			DoRequest<IGetBucketsRequest, GetBucketsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetBucketsResponse> GetBucketsAsync(
			Id jobId,
			Func<GetBucketsDescriptor, IGetBucketsRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			GetBucketsAsync(selector.InvokeOrDefault(new GetBucketsDescriptor(jobId)), cancellationToken);

		/// <inheritdoc />
		public Task<GetBucketsResponse> GetBucketsAsync(IGetBucketsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetBucketsRequest, GetBucketsResponse, GetBucketsResponse>(request, request.RequestParameters, ct);
	}
}
