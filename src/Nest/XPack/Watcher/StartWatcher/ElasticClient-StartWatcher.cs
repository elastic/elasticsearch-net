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

		/// <inheritdoc />
		IStartWatcherResponse StartWatcher(IStartWatcherRequest request);

		/// <inheritdoc />
		Task<IStartWatcherResponse> StartWatcherAsync(Func<StartWatcherDescriptor, IStartWatcherRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IStartWatcherResponse> StartWatcherAsync(IStartWatcherRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IStartWatcherResponse StartWatcher(Func<StartWatcherDescriptor, IStartWatcherRequest> selector = null) =>
			StartWatcher(selector.InvokeOrDefault(new StartWatcherDescriptor()));

		/// <inheritdoc />
		public IStartWatcherResponse StartWatcher(IStartWatcherRequest request) =>
			Dispatcher.Dispatch<IStartWatcherRequest, StartWatcherRequestParameters, StartWatcherResponse>(
				request,
				(p, d) => LowLevelDispatch.WatcherStartDispatch<StartWatcherResponse>(p)
			);

		/// <inheritdoc />
		public Task<IStartWatcherResponse> StartWatcherAsync(Func<StartWatcherDescriptor, IStartWatcherRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			StartWatcherAsync(selector.InvokeOrDefault(new StartWatcherDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IStartWatcherResponse> StartWatcherAsync(IStartWatcherRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IStartWatcherRequest, StartWatcherRequestParameters, StartWatcherResponse, IStartWatcherResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.WatcherStartDispatchAsync<StartWatcherResponse>(p, c)
			);
	}
}
