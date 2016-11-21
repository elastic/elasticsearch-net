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
		Task<IGetWatchResponse> GetWatchAsync(Id watchId, Func<GetWatchDescriptor, IGetWatchRequest> selector = null);

		/// <inheritdoc/>
		Task<IGetWatchResponse> GetWatchAsync(IGetWatchRequest request);
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
				(p, d) => this.LowLevelDispatch.WatcherGetWatchDispatch<GetWatchResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IGetWatchResponse> GetWatchAsync(Id watchId, Func<GetWatchDescriptor, IGetWatchRequest> selector = null) =>
			this.GetWatchAsync(selector.InvokeOrDefault(new GetWatchDescriptor(watchId)));

		/// <inheritdoc/>
		public Task<IGetWatchResponse> GetWatchAsync(IGetWatchRequest request) =>
			this.Dispatcher.DispatchAsync<IGetWatchRequest, GetWatchRequestParameters, GetWatchResponse, IGetWatchResponse>(
				request,
				(p, d) => this.LowLevelDispatch.WatcherGetWatchDispatchAsync<GetWatchResponse>(p)
			);
	}
}
