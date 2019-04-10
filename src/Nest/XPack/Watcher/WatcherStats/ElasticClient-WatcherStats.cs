using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Gets the current Watcher metrics
		/// </summary>
		IWatcherStatsResponse WatcherStats(Func<WatcherStatsDescriptor, IWatcherStatsRequest> selector = null);

		/// <inheritdoc />
		IWatcherStatsResponse WatcherStats(IWatcherStatsRequest request);

		/// <inheritdoc />
		Task<IWatcherStatsResponse> WatcherStatsAsync(Func<WatcherStatsDescriptor, IWatcherStatsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IWatcherStatsResponse> WatcherStatsAsync(IWatcherStatsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IWatcherStatsResponse WatcherStats(Func<WatcherStatsDescriptor, IWatcherStatsRequest> selector = null) =>
			WatcherStats(selector.InvokeOrDefault(new WatcherStatsDescriptor()));

		/// <inheritdoc />
		public IWatcherStatsResponse WatcherStats(IWatcherStatsRequest request) =>
			DoRequest<IWatcherStatsRequest, WatcherStatsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IWatcherStatsResponse> WatcherStatsAsync(
			Func<WatcherStatsDescriptor, IWatcherStatsRequest> selector = null,
			CancellationToken ct = default
		) => WatcherStatsAsync(selector.InvokeOrDefault(new WatcherStatsDescriptor()), ct);

		/// <inheritdoc />
		public Task<IWatcherStatsResponse> WatcherStatsAsync(IWatcherStatsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IWatcherStatsRequest, IWatcherStatsResponse, WatcherStatsResponse>
				(request, request.RequestParameters, ct);
	}
}
