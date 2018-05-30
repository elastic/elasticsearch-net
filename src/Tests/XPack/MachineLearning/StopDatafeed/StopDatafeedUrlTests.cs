using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.StopDatafeed
{
	public class StopDatafeedUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/_xpack/ml/datafeeds/datafeed_id/_stop")
				.Fluent(c => c.StopDatafeed("datafeed_id"))
				.Request(c => c.StopDatafeed(new StopDatafeedRequest("datafeed_id")))
				.FluentAsync(c => c.StopDatafeedAsync("datafeed_id"))
				.RequestAsync(c => c.StopDatafeedAsync(new StopDatafeedRequest("datafeed_id")))
				;
		}
	}
}
