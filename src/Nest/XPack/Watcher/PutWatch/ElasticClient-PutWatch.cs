using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Registers a new watch in Watcher or updates an existing one.
		/// Once registered, a new document will be added to the .watches index, representing the watch,
		/// and its trigger will immediately be registered with the relevant trigger engine.
		/// </summary>
		IPutWatchResponse PutWatch(Id watchId, Func<PutWatchDescriptor, IPutWatchRequest> selector = null);

		/// <inheritdoc/>
		IPutWatchResponse PutWatch(IPutWatchRequest request);

		/// <inheritdoc/>
		Task<IPutWatchResponse> PutWatchAsync(Id watchId, Func<PutWatchDescriptor, IPutWatchRequest> selector = null);

		/// <inheritdoc/>
		Task<IPutWatchResponse> PutWatchAsync(IPutWatchRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IPutWatchResponse PutWatch(Id watchId, Func<PutWatchDescriptor, IPutWatchRequest> selector = null) =>
			this.PutWatch(selector.InvokeOrDefault(new PutWatchDescriptor(watchId)));

		/// <inheritdoc/>
		public IPutWatchResponse PutWatch(IPutWatchRequest request) =>
			this.Dispatcher.Dispatch<IPutWatchRequest, PutWatchRequestParameters, PutWatchResponse>(
				request,
				this.LowLevelDispatch.WatcherPutWatchDispatch<PutWatchResponse>
			);

		/// <inheritdoc/>
		public Task<IPutWatchResponse> PutWatchAsync(Id watchId, Func<PutWatchDescriptor, IPutWatchRequest> selector = null) =>
			this.PutWatchAsync(selector.InvokeOrDefault(new PutWatchDescriptor(watchId)));

		/// <inheritdoc/>
		public Task<IPutWatchResponse> PutWatchAsync(IPutWatchRequest request) =>
			this.Dispatcher.DispatchAsync<IPutWatchRequest, PutWatchRequestParameters, PutWatchResponse, IPutWatchResponse>(
				request,
				this.LowLevelDispatch.WatcherPutWatchDispatchAsync<PutWatchResponse>
			);
	}
}
