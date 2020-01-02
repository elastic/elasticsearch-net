using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.Watcher.WatcherStats
{
	public class WatcherStatsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_watcher/stats")
					.Fluent(c => c.Watcher.Stats())
					.Request(c => c.Watcher.Stats(new WatcherStatsRequest()))
					.FluentAsync(c => c.Watcher.StatsAsync())
					.RequestAsync(c => c.Watcher.StatsAsync(new WatcherStatsRequest()))
				;

			await GET("/_watcher/stats/_all")
					.Fluent(c => c.Watcher.Stats(r => r.Metric(WatcherStatsMetric.All)))
					.Request(c => c.Watcher.Stats(new WatcherStatsRequest(WatcherStatsMetric.All)))
					.FluentAsync(c => c.Watcher.StatsAsync(r => r.Metric(WatcherStatsMetric.All)))
					.RequestAsync(c => c.Watcher.StatsAsync(new WatcherStatsRequest(WatcherStatsMetric.All)))
				;

			var metrics = WatcherStatsMetric.QueuedWatches | WatcherStatsMetric.CurrentWatches;
			await GET("/_watcher/stats/queued_watches%2Ccurrent_watches")
					.Fluent(c => c.Watcher.Stats(r => r.Metric(metrics)))
					.Request(c => c.Watcher.Stats(new WatcherStatsRequest(metrics)))
					.FluentAsync(c => c.Watcher.StatsAsync(r => r.Metric(metrics)))
					.RequestAsync(c => c.Watcher.StatsAsync(new WatcherStatsRequest(metrics)))
				;
		}
	}
}
