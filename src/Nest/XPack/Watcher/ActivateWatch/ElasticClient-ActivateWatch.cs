using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Activates a currently inactive watch.
		/// </summary>
		IActivateWatchResponse ActivateWatch(Id id, Func<ActivateWatchDescriptor, IActivateWatchRequest> selector = null);

		/// <inheritdoc/>
		IActivateWatchResponse ActivateWatch(IActivateWatchRequest request);

		/// <inheritdoc/>
		Task<IActivateWatchResponse> ActivateWatchAsync(Id id, Func<ActivateWatchDescriptor, IActivateWatchRequest> selector = null);

		/// <inheritdoc/>
		Task<IActivateWatchResponse> ActivateWatchAsync(IActivateWatchRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IActivateWatchResponse ActivateWatch(Id id, Func<ActivateWatchDescriptor, IActivateWatchRequest> selector = null) =>
			this.ActivateWatch(selector.InvokeOrDefault(new ActivateWatchDescriptor(id)));

		/// <inheritdoc/>
		public IActivateWatchResponse ActivateWatch(IActivateWatchRequest request) =>
			this.Dispatcher.Dispatch<IActivateWatchRequest, ActivateWatchRequestParameters, ActivateWatchResponse>(
				request,
				(p, d) => this.LowLevelDispatch.WatcherActivateWatchDispatch<ActivateWatchResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IActivateWatchResponse> ActivateWatchAsync(Id id, Func<ActivateWatchDescriptor, IActivateWatchRequest> selector = null) =>
			this.ActivateWatchAsync(selector.InvokeOrDefault(new ActivateWatchDescriptor(id)));

		/// <inheritdoc/>
		public Task<IActivateWatchResponse> ActivateWatchAsync(IActivateWatchRequest request) =>
			this.Dispatcher.DispatchAsync<IActivateWatchRequest, ActivateWatchRequestParameters, ActivateWatchResponse, IActivateWatchResponse>(
				request,
				(p, d) => this.LowLevelDispatch.WatcherActivateWatchDispatchAsync<ActivateWatchResponse>(p)
			);
	}
}
