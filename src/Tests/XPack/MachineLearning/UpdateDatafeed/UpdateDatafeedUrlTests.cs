using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.UpdateDatafeed
{
	public class UpdateDatafeedUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/_xpack/ml/datafeeds/datafeed_id/_update")
				.Fluent(c => c.UpdateDatafeed<object>("datafeed_id", p => p))
				.Request(c => c.UpdateDatafeed(new UpdateDatafeedRequest("datafeed_id")))
				.FluentAsync(c => c.UpdateDatafeedAsync<object>("datafeed_id"))
				.RequestAsync(c => c.UpdateDatafeedAsync(new UpdateDatafeedRequest("datafeed_id")))
				;
		}
	}
}
