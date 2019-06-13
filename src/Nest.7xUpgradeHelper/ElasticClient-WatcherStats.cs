using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Gets the current Watcher metrics
		/// </summary>
		public static WatcherStatsResponse WatcherStats(this IElasticClient client,Func<WatcherStatsDescriptor, IWatcherStatsRequest> selector = null);

		/// <inheritdoc />
		public static WatcherStatsResponse WatcherStats(this IElasticClient client,IWatcherStatsRequest request);

		/// <inheritdoc />
		public static Task<WatcherStatsResponse> WatcherStatsAsync(this IElasticClient client,Func<WatcherStatsDescriptor, IWatcherStatsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<WatcherStatsResponse> WatcherStatsAsync(this IElasticClient client,IWatcherStatsRequest request, CancellationToken ct = default);
	}

}
