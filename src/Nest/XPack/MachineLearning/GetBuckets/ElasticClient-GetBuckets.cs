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

		/// <inheritdoc/>
		IGetBucketsResponse GetBuckets(IGetBucketsRequest request);

		/// <inheritdoc/>
		Task<IGetBucketsResponse> GetBucketsAsync(Id jobId, Func<GetBucketsDescriptor, IGetBucketsRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IGetBucketsResponse> GetBucketsAsync(IGetBucketsRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetBucketsResponse GetBuckets(Id jobId, Func<GetBucketsDescriptor, IGetBucketsRequest> selector = null) =>
			this.GetBuckets(selector.InvokeOrDefault(new GetBucketsDescriptor(jobId)));

		/// <inheritdoc/>
		public IGetBucketsResponse GetBuckets(IGetBucketsRequest request) =>
			this.Dispatcher.Dispatch<IGetBucketsRequest, GetBucketsRequestParameters, GetBucketsResponse>(
				request,
				this.LowLevelDispatch.XpackMlGetBucketsDispatch<GetBucketsResponse>
			);

		/// <inheritdoc/>
		public Task<IGetBucketsResponse> GetBucketsAsync(Id jobId, Func<GetBucketsDescriptor, IGetBucketsRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.GetBucketsAsync(selector.InvokeOrDefault(new GetBucketsDescriptor(jobId)), cancellationToken);

		/// <inheritdoc/>
		public Task<IGetBucketsResponse> GetBucketsAsync(IGetBucketsRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IGetBucketsRequest, GetBucketsRequestParameters, GetBucketsResponse, IGetBucketsResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.XpackMlGetBucketsDispatchAsync<GetBucketsResponse>
			);
	}
}
