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
		Task<IGetOverallBucketsResponse> GetOverallBucketsAsync(Id jobId, Func<GetOverallBucketsDescriptor, IGetOverallBucketsRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		/// Retrieves machine learning job results for one or more buckets.
		/// </summary>
		Task<IGetOverallBucketsResponse> GetOverallBucketsAsync(IGetOverallBucketsRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetOverallBucketsResponse GetOverallBuckets(Id jobId, Func<GetOverallBucketsDescriptor, IGetOverallBucketsRequest> selector = null) =>
			this.GetOverallBuckets(selector.InvokeOrDefault(new GetOverallBucketsDescriptor(jobId)));

		/// <inheritdoc/>
		public IGetOverallBucketsResponse GetOverallBuckets(IGetOverallBucketsRequest request) =>
			this.Dispatcher.Dispatch<IGetOverallBucketsRequest, GetOverallBucketsRequestParameters, GetOverallBucketsResponse>(
				request,
				this.LowLevelDispatch.XpackMlGetOverallBucketsDispatch<GetOverallBucketsResponse>
			);

		/// <inheritdoc/>
		public Task<IGetOverallBucketsResponse> GetOverallBucketsAsync(Id jobId, Func<GetOverallBucketsDescriptor, IGetOverallBucketsRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.GetOverallBucketsAsync(selector.InvokeOrDefault(new GetOverallBucketsDescriptor(jobId)), cancellationToken);

		/// <inheritdoc/>
		public Task<IGetOverallBucketsResponse> GetOverallBucketsAsync(IGetOverallBucketsRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IGetOverallBucketsRequest, GetOverallBucketsRequestParameters, GetOverallBucketsResponse, IGetOverallBucketsResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.XpackMlGetOverallBucketsDispatchAsync<GetOverallBucketsResponse>
			);
	}
}
