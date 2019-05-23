using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.UpdateDatafeed
{
	public class UpdateDatafeedUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_ml/datafeeds/datafeed_id/_update")
			.Fluent(c => c.MachineLearning.UpdateDatafeed<object>("datafeed_id", p => p))
			.Request(c => c.MachineLearning.UpdateDatafeed(new UpdateDatafeedRequest("datafeed_id")))
			.FluentAsync(c => c.MachineLearning.UpdateDatafeedAsync<object>("datafeed_id"))
			.RequestAsync(c => c.MachineLearning.UpdateDatafeedAsync(new UpdateDatafeedRequest("datafeed_id")));
	}
}
