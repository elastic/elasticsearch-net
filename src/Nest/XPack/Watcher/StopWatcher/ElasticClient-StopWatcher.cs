using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Stops the Watcher/Alerting service, if the service is running
		/// </summary>
		IStopWatcherResponse StopWatcher(Func<StopWatcherDescriptor, IStopWatcherRequest> selector = null);

		/// <inheritdoc/>
		IStopWatcherResponse StopWatcher(IStopWatcherRequest request);

		/// <inheritdoc/>
		Task<IStopWatcherResponse> StopWatcherAsync(Func<StopWatcherDescriptor, IStopWatcherRequest> selector = null);

		/// <inheritdoc/>
		Task<IStopWatcherResponse> StopWatcherAsync(IStopWatcherRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IStopWatcherResponse StopWatcher(Func<StopWatcherDescriptor, IStopWatcherRequest> selector = null) =>
			this.StopWatcher(selector.InvokeOrDefault(new StopWatcherDescriptor()));

		/// <inheritdoc/>
		public IStopWatcherResponse StopWatcher(IStopWatcherRequest request) =>
			this.Dispatcher.Dispatch<IStopWatcherRequest, StopWatcherRequestParameters, StopWatcherResponse>(
				request,
				(p, d) => this.LowLevelDispatch.WatcherStopDispatch<StopWatcherResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IStopWatcherResponse> StopWatcherAsync(Func<StopWatcherDescriptor, IStopWatcherRequest> selector = null) =>
			this.StopWatcherAsync(selector.InvokeOrDefault(new StopWatcherDescriptor()));

		/// <inheritdoc/>
		public Task<IStopWatcherResponse> StopWatcherAsync(IStopWatcherRequest request) =>
			this.Dispatcher.DispatchAsync<IStopWatcherRequest, StopWatcherRequestParameters, StopWatcherResponse, IStopWatcherResponse>(
				request,
				(p, d) => this.LowLevelDispatch.WatcherStopDispatchAsync<StopWatcherResponse>(p)
			);
	}
}
