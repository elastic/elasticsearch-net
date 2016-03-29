using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cluster.NodesStats
{
	public class NodesStatsUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_nodes/stats")
				.Fluent(c => c.NodesStats())
				.Request(c => c.NodesStats(new NodesStatsRequest()))
				.FluentAsync(c => c.NodesStatsAsync())
				.RequestAsync(c => c.NodesStatsAsync(new NodesStatsRequest()))
				;

			await GET("/_nodes/foo/stats")
				.Fluent(c => c.NodesStats(n => n.NodeId("foo")))
				.Request(c => c.NodesStats(new NodesStatsRequest("foo")))
				.FluentAsync(c => c.NodesStatsAsync(n => n.NodeId("foo")))
				.RequestAsync(c => c.NodesStatsAsync(new NodesStatsRequest("foo")))
				;

			var metrics = NodesStatsMetric.Fs | NodesStatsMetric.Jvm;
			await GET("/_nodes/stats/fs%2Cjvm")
				.Fluent(c => c.NodesStats(p=>p.Metric(metrics)))
				.Request(c => c.NodesStats(new NodesStatsRequest(metrics)))
				.FluentAsync(c => c.NodesStatsAsync(p=>p.Metric(metrics)))
				.RequestAsync(c => c.NodesStatsAsync(new NodesStatsRequest(metrics)))
				;

			await GET("/_nodes/foo/stats/fs%2Cjvm")
				.Fluent(c => c.NodesStats(p=>p.NodeId("foo").Metric(metrics)))
				.Request(c => c.NodesStats(new NodesStatsRequest("foo", metrics)))
				.FluentAsync(c => c.NodesStatsAsync(p=>p.NodeId("foo").Metric(metrics)))
				.RequestAsync(c => c.NodesStatsAsync(new NodesStatsRequest("foo", metrics)))
				;

			var indexMetrics = NodesStatsIndexMetric.Fielddata | NodesStatsIndexMetric.Merge;
			await GET("/_nodes/stats/fs%2Cjvm/fielddata%2Cmerge")
				.Fluent(c => c.NodesStats(p=>p.Metric(metrics).IndexMetric(indexMetrics)))
				.Request(c => c.NodesStats(new NodesStatsRequest(metrics, indexMetrics)))
				.FluentAsync(c => c.NodesStatsAsync(p=>p.Metric(metrics).IndexMetric(indexMetrics)))
				.RequestAsync(c => c.NodesStatsAsync(new NodesStatsRequest(metrics, indexMetrics)))
				;

			await GET("/_nodes/foo/stats/fs%2Cjvm/fielddata%2Cmerge")
				.Fluent(c => c.NodesStats(p=>p.NodeId("foo").Metric(metrics).IndexMetric(indexMetrics)))
				.Request(c => c.NodesStats(new NodesStatsRequest("foo", metrics, indexMetrics)))
				.FluentAsync(c => c.NodesStatsAsync(p=>p.NodeId("foo").Metric(metrics).IndexMetric(indexMetrics)))
				.RequestAsync(c => c.NodesStatsAsync(new NodesStatsRequest("foo", metrics, indexMetrics)))
				;
		}
	}
}
