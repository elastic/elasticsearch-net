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

		/// <inheritdoc cref="PutWatch(Nest.Id,System.Func{Nest.PutWatchDescriptor,Nest.IPutWatchRequest})"/>
		IPutWatchResponse PutWatch(IPutWatchRequest request);

		/// <inheritdoc cref="PutWatch(Nest.Id,System.Func{Nest.PutWatchDescriptor,Nest.IPutWatchRequest})"/>
		Task<IPutWatchResponse> PutWatchAsync(Id watchId, Func<PutWatchDescriptor, IPutWatchRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc cref="PutWatch(Nest.Id,System.Func{Nest.PutWatchDescriptor,Nest.IPutWatchRequest})"/>
		Task<IPutWatchResponse> PutWatchAsync(IPutWatchRequest request, CancellationToken cancellationToken = default(CancellationToken));
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
				this.LowLevelDispatch.XpackWatcherPutWatchDispatch<PutWatchResponse>
			);

		/// <inheritdoc/>
		public Task<IPutWatchResponse> PutWatchAsync(Id watchId, Func<PutWatchDescriptor, IPutWatchRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.PutWatchAsync(selector.InvokeOrDefault(new PutWatchDescriptor(watchId)), cancellationToken);

		/// <inheritdoc/>
		public Task<IPutWatchResponse> PutWatchAsync(IPutWatchRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IPutWatchRequest, PutWatchRequestParameters, PutWatchResponse, IPutWatchResponse>(
				request,
				cancellationToken,
				this.LowLevelDispatch.XpackWatcherPutWatchDispatchAsync<PutWatchResponse>
			);
	}
}
