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
		Task<IStopWatcherResponse> StopWatcherAsync(Func<StopWatcherDescriptor, IStopWatcherRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IStopWatcherResponse> StopWatcherAsync(IStopWatcherRequest request, CancellationToken cancellationToken = default(CancellationToken));
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
				(p, d) => this.LowLevelDispatch.XpackWatcherStopDispatch<StopWatcherResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IStopWatcherResponse> StopWatcherAsync(Func<StopWatcherDescriptor, IStopWatcherRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.StopWatcherAsync(selector.InvokeOrDefault(new StopWatcherDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IStopWatcherResponse> StopWatcherAsync(IStopWatcherRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IStopWatcherRequest, StopWatcherRequestParameters, StopWatcherResponse, IStopWatcherResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackWatcherStopDispatchAsync<StopWatcherResponse>(p,c)
			);
	}
}
