using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Modules.Indices.Monitoring.IndicesStats
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
			await GET($"/{index}/_stats")
				.Fluent(c => c.IndicesStats(index))
				.Request(c => c.IndicesStats(new IndicesStatsRequest(index)))
				.FluentAsync(c => c.IndicesStatsAsync(index))
				.RequestAsync(c => c.IndicesStatsAsync(new IndicesStatsRequest(index)))
				;

			//var index = "index1,index2";
			//await GET($"/{index}/_stats")
			//	.Fluent(c => c.IndicesStats(index))
			//	.Request(c => c.IndicesStats(new IndicesStatsRequest(index, )))
			//	.FluentAsync(c => c.IndicesStatsAsync(index))
			//	.RequestAsync(c => c.IndicesStatsAsync(new IndicesStatsRequest(index)))
			//	;
		}
	}
}
