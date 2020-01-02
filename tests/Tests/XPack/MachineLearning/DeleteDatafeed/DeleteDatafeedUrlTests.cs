using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.DeleteDatafeed
{
	public class DeleteDatafeedUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await DELETE("/_ml/datafeeds/datafeed_id")
			.Fluent(c => c.MachineLearning.DeleteDatafeed("datafeed_id"))
			.Request(c => c.MachineLearning.DeleteDatafeed(new DeleteDatafeedRequest("datafeed_id")))
			.FluentAsync(c => c.MachineLearning.DeleteDatafeedAsync("datafeed_id"))
			.RequestAsync(c => c.MachineLearning.DeleteDatafeedAsync(new DeleteDatafeedRequest("datafeed_id")));
	}
}
