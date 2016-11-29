using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Starts the Watcher/Alerting service, if the service is not already running
		/// </summary>
		IStartWatcherResponse StartWatcher(Func<StartWatcherDescriptor, IStartWatcherRequest> selector = null);

		/// <inheritdoc/>
		IStartWatcherResponse StartWatcher(IStartWatcherRequest request);

		/// <inheritdoc/>
		Task<IStartWatcherResponse> StartWatcherAsync(Func<StartWatcherDescriptor, IStartWatcherRequest> selector = null);

		/// <inheritdoc/>
		Task<IStartWatcherResponse> StartWatcherAsync(IStartWatcherRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IStartWatcherResponse StartWatcher(Func<StartWatcherDescriptor, IStartWatcherRequest> selector = null) =>
			this.StartWatcher(selector.InvokeOrDefault(new StartWatcherDescriptor()));

		/// <inheritdoc/>
		public IStartWatcherResponse StartWatcher(IStartWatcherRequest request) =>
			this.Dispatcher.Dispatch<IStartWatcherRequest, StartWatcherRequestParameters, StartWatcherResponse>(
				request,
				(p, d) => this.LowLevelDispatch.WatcherStartDispatch<StartWatcherResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IStartWatcherResponse> StartWatcherAsync(Func<StartWatcherDescriptor, IStartWatcherRequest> selector = null) =>
			this.StartWatcherAsync(selector.InvokeOrDefault(new StartWatcherDescriptor()));

		/// <inheritdoc/>
		public Task<IStartWatcherResponse> StartWatcherAsync(IStartWatcherRequest request) =>
			this.Dispatcher.DispatchAsync<IStartWatcherRequest, StartWatcherRequestParameters, StartWatcherResponse, IStartWatcherResponse>(
				request,
				(p, d) => this.LowLevelDispatch.WatcherStartDispatchAsync<StartWatcherResponse>(p)
			);
	}
}
