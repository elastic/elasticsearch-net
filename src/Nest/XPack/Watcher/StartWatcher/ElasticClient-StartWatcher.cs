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
		Task<IStartWatcherResponse> StartWatcherAsync(Func<StartWatcherDescriptor, IStartWatcherRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IStartWatcherResponse> StartWatcherAsync(IStartWatcherRequest request, CancellationToken cancellationToken = default(CancellationToken));
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
				(p, d) => this.LowLevelDispatch.XpackWatcherStartDispatch<StartWatcherResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IStartWatcherResponse> StartWatcherAsync(Func<StartWatcherDescriptor, IStartWatcherRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.StartWatcherAsync(selector.InvokeOrDefault(new StartWatcherDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IStartWatcherResponse> StartWatcherAsync(IStartWatcherRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IStartWatcherRequest, StartWatcherRequestParameters, StartWatcherResponse, IStartWatcherResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackWatcherStartDispatchAsync<StartWatcherResponse>(p,c)
			);
	}
}
