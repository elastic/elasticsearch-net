﻿using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cluster.NodesStats
{
	public class NodesStatsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_nodes/stats")
					.Fluent(c => c.Nodes.Stats())
					.Request(c => c.Nodes.Stats(new NodesStatsRequest()))
					.FluentAsync(c => c.Nodes.StatsAsync())
					.RequestAsync(c => c.Nodes.StatsAsync(new NodesStatsRequest()))
				;

			await GET("/_nodes/foo/stats")
					.Fluent(c => c.Nodes.Stats(n => n.NodeId("foo")))
					.Request(c => c.Nodes.Stats(new NodesStatsRequest("foo")))
					.FluentAsync(c => c.Nodes.StatsAsync(n => n.NodeId("foo")))
					.RequestAsync(c => c.Nodes.StatsAsync(new NodesStatsRequest("foo")))
				;

			var metrics = NodesStatsMetric.Fs | NodesStatsMetric.Jvm;
			await GET("/_nodes/stats/fs%2Cjvm")
					.Fluent(c => c.Nodes.Stats(p => p.Metric(metrics)))
					.Request(c => c.Nodes.Stats(new NodesStatsRequest(metrics)))
					.FluentAsync(c => c.Nodes.StatsAsync(p => p.Metric(metrics)))
					.RequestAsync(c => c.Nodes.StatsAsync(new NodesStatsRequest(metrics)))
				;

			await GET("/_nodes/foo/stats/fs%2Cjvm")
					.Fluent(c => c.Nodes.Stats(p => p.NodeId("foo").Metric(metrics)))
					.Request(c => c.Nodes.Stats(new NodesStatsRequest("foo", metrics)))
					.FluentAsync(c => c.Nodes.StatsAsync(p => p.NodeId("foo").Metric(metrics)))
					.RequestAsync(c => c.Nodes.StatsAsync(new NodesStatsRequest("foo", metrics)))
				;

			var indexMetrics = NodesStatsIndexMetric.Fielddata | NodesStatsIndexMetric.Merge;
			await GET("/_nodes/stats/fs%2Cjvm/fielddata%2Cmerge")
					.Fluent(c => c.Nodes.Stats(p => p.Metric(metrics).IndexMetric(indexMetrics)))
					.Request(c => c.Nodes.Stats(new NodesStatsRequest(metrics, indexMetrics)))
					.FluentAsync(c => c.Nodes.StatsAsync(p => p.Metric(metrics).IndexMetric(indexMetrics)))
					.RequestAsync(c => c.Nodes.StatsAsync(new NodesStatsRequest(metrics, indexMetrics)))
				;

			await GET("/_nodes/foo/stats/fs%2Cjvm/fielddata%2Cmerge")
					.Fluent(c => c.Nodes.Stats(p => p.NodeId("foo").Metric(metrics).IndexMetric(indexMetrics)))
					.Request(c => c.Nodes.Stats(new NodesStatsRequest("foo", metrics, indexMetrics)))
					.FluentAsync(c => c.Nodes.StatsAsync(p => p.NodeId("foo").Metric(metrics).IndexMetric(indexMetrics)))
					.RequestAsync(c => c.Nodes.StatsAsync(new NodesStatsRequest("foo", metrics, indexMetrics)))
				;
		}
	}
}
