using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.PutDatafeed
{
	public class PutDatafeedUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await PUT("/_xpack/ml/datafeeds/datafeed_id")
				.Fluent(c => c.PutDatafeed<object>("datafeed_id", p => p))
				.Request(c => c.PutDatafeed(new PutDatafeedRequest("datafeed_id")))
				.FluentAsync(c => c.PutDatafeedAsync<object>("datafeed_id"))
				.RequestAsync(c => c.PutDatafeedAsync(new PutDatafeedRequest("datafeed_id")))
				;
		}
	}
}
