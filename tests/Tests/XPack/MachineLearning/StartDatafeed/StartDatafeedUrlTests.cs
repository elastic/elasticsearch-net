using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.StartDatafeed
{
	public class StartDatafeedUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_ml/datafeeds/datafeed_id/_start")
			.Fluent(c => c.MachineLearning.StartDatafeed("datafeed_id"))
			.Request(c => c.MachineLearning.StartDatafeed(new StartDatafeedRequest("datafeed_id")))
			.FluentAsync(c => c.MachineLearning.StartDatafeedAsync("datafeed_id"))
			.RequestAsync(c => c.MachineLearning.StartDatafeedAsync(new StartDatafeedRequest("datafeed_id")));
	}
}
