using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Watcher.WatcherStats
{
	public class WatcherStatsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_watcher/stats")
					.Fluent(c => c.WatcherStats())
					.Request(c => c.WatcherStats(new WatcherStatsRequest()))
					.FluentAsync(c => c.WatcherStatsAsync())
					.RequestAsync(c => c.WatcherStatsAsync(new WatcherStatsRequest()))
				;

			await GET("/_watcher/stats")
					.Fluent(c => c.WatcherStats(r => r.Metric(WatcherStatsMetric.All)))
					.Request(c => c.WatcherStats(new WatcherStatsRequest(WatcherStatsMetric.All)))
					.FluentAsync(c => c.WatcherStatsAsync(r => r.Metric(WatcherStatsMetric.All)))
					.RequestAsync(c => c.WatcherStatsAsync(new WatcherStatsRequest(WatcherStatsMetric.All)))
				;

			var metrics = WatcherStatsMetric.QueuedWatches | WatcherStatsMetric.CurrentWatches;
			await GET("/_watcher/stats/queued_watches%2Ccurrent_watches")
					.Fluent(c => c.WatcherStats(r => r.Metric(metrics)))
					.Request(c => c.WatcherStats(new WatcherStatsRequest(metrics)))
					.FluentAsync(c => c.WatcherStatsAsync(r => r.Metric(metrics)))
					.RequestAsync(c => c.WatcherStatsAsync(new WatcherStatsRequest(metrics)))
				;
		}
	}
}
