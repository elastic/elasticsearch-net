using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.GetDatafeeds
{
	public class GetDatafeedsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_ml/datafeeds")
					.Fluent(c => c.MachineLearning.GetDatafeeds())
					.Request(c => c.MachineLearning.GetDatafeeds(new GetDatafeedsRequest()))
					.FluentAsync(c => c.MachineLearning.GetDatafeedsAsync())
					.RequestAsync(c => c.MachineLearning.GetDatafeedsAsync(new GetDatafeedsRequest()))
				;

			await GET("/_ml/datafeeds/datafeed_id")
					.Fluent(c => c.MachineLearning.GetDatafeeds(r => r.DatafeedId("datafeed_id")))
					.Request(c => c.MachineLearning.GetDatafeeds(new GetDatafeedsRequest("datafeed_id")))
					.FluentAsync(c => c.MachineLearning.GetDatafeedsAsync(r => r.DatafeedId("datafeed_id")))
					.RequestAsync(c => c.MachineLearning.GetDatafeedsAsync(new GetDatafeedsRequest("datafeed_id")))
				;
		}
	}
}
