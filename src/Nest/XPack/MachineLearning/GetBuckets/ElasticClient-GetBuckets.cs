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
		IGetBucketsResponse GetBuckets(Id jobId, Func<GetBucketsDescriptor, IGetBucketsRequest> selector = null);

		/// <inheritdoc />
		IGetBucketsResponse GetBuckets(IGetBucketsRequest request);

		/// <inheritdoc />
		Task<IGetBucketsResponse> GetBucketsAsync(Id jobId, Func<GetBucketsDescriptor, IGetBucketsRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc />
		Task<IGetBucketsResponse> GetBucketsAsync(IGetBucketsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetBucketsResponse GetBuckets(Id jobId, Func<GetBucketsDescriptor, IGetBucketsRequest> selector = null) =>
			GetBuckets(selector.InvokeOrDefault(new GetBucketsDescriptor(jobId)));

		/// <inheritdoc />
		public IGetBucketsResponse GetBuckets(IGetBucketsRequest request) =>
			DoRequest<IGetBucketsRequest, GetBucketsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IGetBucketsResponse> GetBucketsAsync(
			Id jobId,
			Func<GetBucketsDescriptor, IGetBucketsRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			GetBucketsAsync(selector.InvokeOrDefault(new GetBucketsDescriptor(jobId)), cancellationToken);

		/// <inheritdoc />
		public Task<IGetBucketsResponse> GetBucketsAsync(IGetBucketsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetBucketsRequest, IGetBucketsResponse, GetBucketsResponse>(request, request.RequestParameters, ct);
	}
}
