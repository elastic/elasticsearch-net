using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Stops then restarts the Watcher/Alerting service
		/// </summary>
		IRestartWatcherResponse RestartWatcher(Func<RestartWatcherDescriptor, IRestartWatcherRequest> selector = null);

		/// <inheritdoc/>
		IRestartWatcherResponse RestartWatcher(IRestartWatcherRequest request);

		/// <inheritdoc/>
		Task<IRestartWatcherResponse> RestartWatcherAsync(Func<RestartWatcherDescriptor, IRestartWatcherRequest> selector = null);

		/// <inheritdoc/>
		Task<IRestartWatcherResponse> RestartWatcherAsync(IRestartWatcherRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IRestartWatcherResponse RestartWatcher(Func<RestartWatcherDescriptor, IRestartWatcherRequest> selector = null) =>
			this.RestartWatcher(selector.InvokeOrDefault(new RestartWatcherDescriptor()));

		/// <inheritdoc/>
		public IRestartWatcherResponse RestartWatcher(IRestartWatcherRequest request) =>
			this.Dispatcher.Dispatch<IRestartWatcherRequest, RestartWatcherRequestParameters, RestartWatcherResponse>(
				request,
				(p, d) => this.LowLevelDispatch.WatcherRestartDispatch<RestartWatcherResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IRestartWatcherResponse> RestartWatcherAsync(Func<RestartWatcherDescriptor, IRestartWatcherRequest> selector = null) =>
			this.RestartWatcherAsync(selector.InvokeOrDefault(new RestartWatcherDescriptor()));

		/// <inheritdoc/>
		public Task<IRestartWatcherResponse> RestartWatcherAsync(IRestartWatcherRequest request) =>
			this.Dispatcher.DispatchAsync<IRestartWatcherRequest, RestartWatcherRequestParameters, RestartWatcherResponse, IRestartWatcherResponse>(
				request,
				(p, d) => this.LowLevelDispatch.WatcherRestartDispatchAsync<RestartWatcherResponse>(p)
			);
	}
}
