using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Gets basic version information about the Watcher plugin installed
		/// </summary>
		IWatcherInfoResponse WatcherInfo(Func<WatcherInfoDescriptor, IWatcherInfoRequest> selector = null);

		/// <inheritdoc/>
		IWatcherInfoResponse WatcherInfo(IWatcherInfoRequest request);

		/// <inheritdoc/>
		Task<IWatcherInfoResponse> WatcherInfoAsync(Func<WatcherInfoDescriptor, IWatcherInfoRequest> selector = null);

		/// <inheritdoc/>
		Task<IWatcherInfoResponse> WatcherInfoAsync(IWatcherInfoRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IWatcherInfoResponse WatcherInfo(Func<WatcherInfoDescriptor, IWatcherInfoRequest> selector = null) =>
			this.WatcherInfo(selector.InvokeOrDefault(new WatcherInfoDescriptor()));

		/// <inheritdoc/>
		public IWatcherInfoResponse WatcherInfo(IWatcherInfoRequest request) =>
			this.Dispatcher.Dispatch<IWatcherInfoRequest, WatcherInfoRequestParameters, WatcherInfoResponse>(
				request,
				(p, d) => this.LowLevelDispatch.WatcherInfoDispatch<WatcherInfoResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IWatcherInfoResponse> WatcherInfoAsync(Func<WatcherInfoDescriptor, IWatcherInfoRequest> selector = null) =>
			this.WatcherInfoAsync(selector.InvokeOrDefault(new WatcherInfoDescriptor()));

		/// <inheritdoc/>
		public Task<IWatcherInfoResponse> WatcherInfoAsync(IWatcherInfoRequest request) =>
			this.Dispatcher.DispatchAsync<IWatcherInfoRequest, WatcherInfoRequestParameters, WatcherInfoResponse, IWatcherInfoResponse>(
				request,
				(p, d) => this.LowLevelDispatch.WatcherInfoDispatchAsync<WatcherInfoResponse>(p)
			);
	}
}
