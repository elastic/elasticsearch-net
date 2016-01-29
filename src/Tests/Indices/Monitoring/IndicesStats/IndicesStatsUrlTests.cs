using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.Monitoring.IndicesStats
{
	public class IndicesStatsUrlTests
	{
		[U] public async Task Urls()
		{
			await GET($"/_stats")
				.Fluent(c => c.IndicesStats(Nest.Indices.All))
				.Request(c => c.IndicesStats(new IndicesStatsRequest()))
				.FluentAsync(c => c.IndicesStatsAsync(Nest.Indices.All))
				.RequestAsync(c => c.IndicesStatsAsync(new IndicesStatsRequest()))
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
				.Fluent(c => c.IndicesStats(index, i=>i.Metric(metrics)))
				.Request(c => c.IndicesStats(new IndicesStatsRequest(index, metrics)))
				.FluentAsync(c => c.IndicesStatsAsync(index, i => i.Metric(metrics)))
				.RequestAsync(c => c.IndicesStatsAsync(new IndicesStatsRequest(index, metrics)))
				;

			metrics = IndicesStatsMetric.Completion | IndicesStatsMetric.Flush | IndicesStatsMetric.All;
			var request = new IndicesStatsRequest(index, metrics)
			{
				Types = new TypeName[] { typeof(Project) }
			};
			await GET($"/index1%2Cindex2/_stats/_all?types=project")
				.Fluent(c => c.IndicesStats(index, i=>i.Metric(metrics).Types(typeof(Project))))
				.Request(c => c.IndicesStats(request))
				.FluentAsync(c => c.IndicesStatsAsync(index, i => i.Metric(metrics).Types(typeof(Project))))
				.RequestAsync(c => c.IndicesStatsAsync(request))
				;
		}
	}
}
