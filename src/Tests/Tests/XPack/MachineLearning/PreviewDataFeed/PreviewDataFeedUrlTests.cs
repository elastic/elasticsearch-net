using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.PreviewDatafeed
{
	public class PreviewDatafeedUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_ml/datafeeds/datafeed_id/_preview")
			.Fluent(c => c.MachineLearning.PreviewDatafeed<Metric>("datafeed_id", p => p))
			.Request(c => c.MachineLearning.PreviewDatafeed<Metric>(new PreviewDatafeedRequest("datafeed_id")))
			.FluentAsync(c => c.MachineLearning.PreviewDatafeedAsync<Metric>("datafeed_id", p => p))
			.RequestAsync(c => c.MachineLearning.PreviewDatafeedAsync<Metric>(new PreviewDatafeedRequest("datafeed_id")));
	}
}
