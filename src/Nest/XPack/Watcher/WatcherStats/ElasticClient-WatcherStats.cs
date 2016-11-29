using System;
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

		/// <inheritdoc/>
		IWatcherStatsResponse WatcherStats(IWatcherStatsRequest request);

		/// <inheritdoc/>
		Task<IWatcherStatsResponse> WatcherStatsAsync(Func<WatcherStatsDescriptor, IWatcherStatsRequest> selector = null);

		/// <inheritdoc/>
		Task<IWatcherStatsResponse> WatcherStatsAsync(IWatcherStatsRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IWatcherStatsResponse WatcherStats(Func<WatcherStatsDescriptor, IWatcherStatsRequest> selector = null) =>
			this.WatcherStats(selector.InvokeOrDefault(new WatcherStatsDescriptor()));

		/// <inheritdoc/>
		public IWatcherStatsResponse WatcherStats(IWatcherStatsRequest request) =>
			this.Dispatcher.Dispatch<IWatcherStatsRequest, WatcherStatsRequestParameters, WatcherStatsResponse>(
				request,
				(p, d) => this.LowLevelDispatch.WatcherStatsDispatch<WatcherStatsResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IWatcherStatsResponse> WatcherStatsAsync(Func<WatcherStatsDescriptor, IWatcherStatsRequest> selector = null) =>
			this.WatcherStatsAsync(selector.InvokeOrDefault(new WatcherStatsDescriptor()));

		/// <inheritdoc/>
		public Task<IWatcherStatsResponse> WatcherStatsAsync(IWatcherStatsRequest request) =>
			this.Dispatcher.DispatchAsync<IWatcherStatsRequest, WatcherStatsRequestParameters, WatcherStatsResponse, IWatcherStatsResponse>(
				request,
				(p, d) => this.LowLevelDispatch.WatcherStatsDispatchAsync<WatcherStatsResponse>(p)
			);
	}
}
