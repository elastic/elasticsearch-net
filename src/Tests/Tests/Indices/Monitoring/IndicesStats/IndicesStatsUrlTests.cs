using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Tests.Domain;
using Tests.Framework;
using static Tests.Framework.UrlTester;
using static Nest.Indices;

namespace Tests.Indices.Monitoring.IndicesStats
{
	public class IndicesStatsUrlTests
	{
		[U] public async Task Urls()
		{
			await GET($"/_stats")
					.Request(c => c.IndicesStats(new IndicesStatsRequest()))
					.RequestAsync(c => c.IndicesStatsAsync(new IndicesStatsRequest()))
				;

			await GET($"/_all/_stats")
					.Fluent(c => c.IndicesStats(All))
					.Request(c => c.IndicesStats(new IndicesStatsRequest(All)))
					.FluentAsync(c => c.IndicesStatsAsync(All))
					.RequestAsync(c => c.IndicesStatsAsync(new IndicesStatsRequest(All)))
				;
			var index = "index1,index2";
			await GET($"/index1%2Cindex2/_stats")
					.Fluent(c => c.IndicesStats(index))
					.Request(c => c.IndicesStats(new IndicesStatsRequest(index)))
					.FluentAsync(c => c.IndicesStatsAsync(index))
					.RequestAsync(c => c.IndicesStatsAsync(new IndicesStatsRequest(index)))
				;

			var metrics = IndicesStatsMetric.Completion | IndicesStatsMetric.Flush;
			await GET($"/index1%2Cindex2/_stats/completion%2Cflush")
					.Fluent(c => c.IndicesStats(index, i => i.Metric(metrics)))
					.Request(c => c.IndicesStats(new IndicesStatsRequest(index, metrics)))
					.FluentAsync(c => c.IndicesStatsAsync(index, i => i.Metric(metrics)))
					.RequestAsync(c => c.IndicesStatsAsync(new IndicesStatsRequest(index, metrics)))
				;

			metrics = IndicesStatsMetric.Completion | IndicesStatsMetric.Flush | IndicesStatsMetric.All;
			var request = new IndicesStatsRequest(index, metrics) { };
			await GET($"/index1%2Cindex2/_stats/_all")
					.Fluent(c => c.IndicesStats(index, i => i.Metric(metrics)))
					.Request(c => c.IndicesStats(request))
					.FluentAsync(c => c.IndicesStatsAsync(index, i => i.Metric(metrics)))
					.RequestAsync(c => c.IndicesStatsAsync(request))
				;
		}
	}
}
