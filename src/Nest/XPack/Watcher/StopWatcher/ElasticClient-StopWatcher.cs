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

		/// <inheritdoc />
		IStopWatcherResponse StopWatcher(IStopWatcherRequest request);

		/// <inheritdoc />
		Task<IStopWatcherResponse> StopWatcherAsync(Func<StopWatcherDescriptor, IStopWatcherRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IStopWatcherResponse> StopWatcherAsync(IStopWatcherRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IStopWatcherResponse StopWatcher(Func<StopWatcherDescriptor, IStopWatcherRequest> selector = null) =>
			StopWatcher(selector.InvokeOrDefault(new StopWatcherDescriptor()));

		/// <inheritdoc />
		public IStopWatcherResponse StopWatcher(IStopWatcherRequest request) =>
			Dispatch2<IStopWatcherRequest, StopWatcherResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IStopWatcherResponse> StopWatcherAsync(
			Func<StopWatcherDescriptor, IStopWatcherRequest> selector = null,
			CancellationToken ct = default
		) => StopWatcherAsync(selector.InvokeOrDefault(new StopWatcherDescriptor()), ct);

		/// <inheritdoc />
		public Task<IStopWatcherResponse> StopWatcherAsync(IStopWatcherRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IStopWatcherRequest, IStopWatcherResponse, StopWatcherResponse>
				(request, request.RequestParameters, ct);
	}
}
