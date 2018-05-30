using System;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.GetDatafeedStats
{
	public class GetDatafeedStatsUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_xpack/ml/datafeeds/_stats")
				.Fluent(c => c.GetDatafeedStats())
				.Request(c => c.GetDatafeedStats(new GetDatafeedStatsRequest()))
				.FluentAsync(c => c.GetDatafeedStatsAsync())
				.RequestAsync(c => c.GetDatafeedStatsAsync(new GetDatafeedStatsRequest()))
				;

			await GET("/_xpack/ml/datafeeds/datafeed_id/_stats")
				.Fluent(c => c.GetDatafeedStats(r => r.DatafeedId("datafeed_id")))
				.Request(c => c.GetDatafeedStats(new GetDatafeedStatsRequest("datafeed_id")))
				.FluentAsync(c => c.GetDatafeedStatsAsync(r => r.DatafeedId("datafeed_id")))
				.RequestAsync(c => c.GetDatafeedStatsAsync(new GetDatafeedStatsRequest("datafeed_id")))
				;
		}
	}
}
