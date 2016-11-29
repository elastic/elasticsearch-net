using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Deactivates a currently active watch.
		/// </summary>
		IDeactivateWatchResponse DeactivateWatch(Id id, Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector = null);

		/// <inheritdoc/>
		IDeactivateWatchResponse DeactivateWatch(IDeactivateWatchRequest request);

		/// <inheritdoc/>
		Task<IDeactivateWatchResponse> DeactivateWatchAsync(Id id, Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector = null);

		/// <inheritdoc/>
		Task<IDeactivateWatchResponse> DeactivateWatchAsync(IDeactivateWatchRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IDeactivateWatchResponse DeactivateWatch(Id id, Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector = null) =>
			this.DeactivateWatch(selector.InvokeOrDefault(new DeactivateWatchDescriptor(id)));

		/// <inheritdoc/>
		public IDeactivateWatchResponse DeactivateWatch(IDeactivateWatchRequest request) =>
			this.Dispatcher.Dispatch<IDeactivateWatchRequest, DeactivateWatchRequestParameters, DeactivateWatchResponse>(
				request,
				(p, d) => this.LowLevelDispatch.WatcherDeactivateWatchDispatch<DeactivateWatchResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IDeactivateWatchResponse> DeactivateWatchAsync(Id id, Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector = null) =>
			this.DeactivateWatchAsync(selector.InvokeOrDefault(new DeactivateWatchDescriptor(id)));

		/// <inheritdoc/>
		public Task<IDeactivateWatchResponse> DeactivateWatchAsync(IDeactivateWatchRequest request) =>
			this.Dispatcher.DispatchAsync<IDeactivateWatchRequest, DeactivateWatchRequestParameters, DeactivateWatchResponse, IDeactivateWatchResponse>(
				request,
				(p, d) => this.LowLevelDispatch.WatcherDeactivateWatchDispatchAsync<DeactivateWatchResponse>(p)
			);
	}
}
