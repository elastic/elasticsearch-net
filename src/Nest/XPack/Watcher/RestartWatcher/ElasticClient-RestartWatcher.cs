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

		/// <inheritdoc />
		IRestartWatcherResponse RestartWatcher(IRestartWatcherRequest request);

		/// <inheritdoc />
		Task<IRestartWatcherResponse> RestartWatcherAsync(Func<RestartWatcherDescriptor, IRestartWatcherRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IRestartWatcherResponse> RestartWatcherAsync(IRestartWatcherRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IRestartWatcherResponse RestartWatcher(Func<RestartWatcherDescriptor, IRestartWatcherRequest> selector = null) =>
			RestartWatcher(selector.InvokeOrDefault(new RestartWatcherDescriptor()));

		/// <inheritdoc />
		public IRestartWatcherResponse RestartWatcher(IRestartWatcherRequest request) =>
			Dispatcher.Dispatch<IRestartWatcherRequest, RestartWatcherRequestParameters, RestartWatcherResponse>(
				request,
				(p, d) => LowLevelDispatch.XpackWatcherRestartDispatch<RestartWatcherResponse>(p)
			);

		/// <inheritdoc />
		public Task<IRestartWatcherResponse> RestartWatcherAsync(Func<RestartWatcherDescriptor, IRestartWatcherRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			RestartWatcherAsync(selector.InvokeOrDefault(new RestartWatcherDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IRestartWatcherResponse> RestartWatcherAsync(IRestartWatcherRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IRestartWatcherRequest, RestartWatcherRequestParameters, RestartWatcherResponse, IRestartWatcherResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.XpackWatcherRestartDispatchAsync<RestartWatcherResponse>(p, c)
			);
	}
}
