using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.StartDatafeed
{
	public class StartDatafeedUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/_xpack/ml/datafeeds/datafeed_id/_start")
				.Fluent(c => c.StartDatafeed("datafeed_id"))
				.Request(c => c.StartDatafeed(new StartDatafeedRequest("datafeed_id")))
				.FluentAsync(c => c.StartDatafeedAsync("datafeed_id"))
				.RequestAsync(c => c.StartDatafeedAsync(new StartDatafeedRequest("datafeed_id")))
				;
		}
	}
}
