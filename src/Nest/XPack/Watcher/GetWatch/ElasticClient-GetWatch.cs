using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Retrieves a watch by its id
		/// </summary>
		IGetWatchResponse GetWatch(Id watchId, Func<GetWatchDescriptor, IGetWatchRequest> selector = null);

		/// <inheritdoc/>
		IGetWatchResponse GetWatch(IGetWatchRequest request);

		/// <inheritdoc/>
		Task<IGetWatchResponse> GetWatchAsync(Id watchId, Func<GetWatchDescriptor, IGetWatchRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IGetWatchResponse> GetWatchAsync(IGetWatchRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetWatchResponse GetWatch(Id watchId, Func<GetWatchDescriptor, IGetWatchRequest> selector = null) =>
			this.GetWatch(selector.InvokeOrDefault(new GetWatchDescriptor(watchId)));

		/// <inheritdoc/>
		public IGetWatchResponse GetWatch(IGetWatchRequest request) =>
			this.Dispatcher.Dispatch<IGetWatchRequest, GetWatchRequestParameters, GetWatchResponse>(
				request,
				(p, d) => this.LowLevelDispatch.XpackWatcherGetWatchDispatch<GetWatchResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetWatchResponse> GetWatchAsync(Id watchId, Func<GetWatchDescriptor, IGetWatchRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.GetWatchAsync(selector.InvokeOrDefault(new GetWatchDescriptor(watchId)), cancellationToken);

		/// <inheritdoc/>
		public Task<IGetWatchResponse> GetWatchAsync(IGetWatchRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IGetWatchRequest, GetWatchRequestParameters, GetWatchResponse, IGetWatchResponse>(
				request,
				cancellationToken,
				(p, d ,c) => this.LowLevelDispatch.XpackWatcherGetWatchDispatchAsync<GetWatchResponse>(p,c)
			);
	}
}
