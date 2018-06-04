using System;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.GetDatafeeds
{
	public class GetDatafeedsUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_xpack/ml/datafeeds")
				.Fluent(c => c.GetDatafeeds())
				.Request(c => c.GetDatafeeds(new GetDatafeedsRequest()))
				.FluentAsync(c => c.GetDatafeedsAsync())
				.RequestAsync(c => c.GetDatafeedsAsync(new GetDatafeedsRequest()))
				;

			await GET("/_xpack/ml/datafeeds/datafeed_id")
				.Fluent(c => c.GetDatafeeds(r => r.DatafeedId("datafeed_id")))
				.Request(c => c.GetDatafeeds(new GetDatafeedsRequest("datafeed_id")))
				.FluentAsync(c => c.GetDatafeedsAsync(r => r.DatafeedId("datafeed_id")))
				.RequestAsync(c => c.GetDatafeedsAsync(new GetDatafeedsRequest("datafeed_id")))
				;
		}
	}
}
