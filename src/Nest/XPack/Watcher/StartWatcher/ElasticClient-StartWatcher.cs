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
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IStartWatcherResponse> StartWatcherAsync(IStartWatcherRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IStartWatcherResponse StartWatcher(Func<StartWatcherDescriptor, IStartWatcherRequest> selector = null) =>
			StartWatcher(selector.InvokeOrDefault(new StartWatcherDescriptor()));

		/// <inheritdoc />
		public IStartWatcherResponse StartWatcher(IStartWatcherRequest request) =>
			DoRequest<IStartWatcherRequest, StartWatcherResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IStartWatcherResponse> StartWatcherAsync(
			Func<StartWatcherDescriptor, IStartWatcherRequest> selector = null,
			CancellationToken ct = default
		) => StartWatcherAsync(selector.InvokeOrDefault(new StartWatcherDescriptor()), ct);

		/// <inheritdoc />
		public Task<IStartWatcherResponse> StartWatcherAsync(IStartWatcherRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IStartWatcherRequest, IStartWatcherResponse, StartWatcherResponse>
				(request, request.RequestParameters, ct);
	}
}
