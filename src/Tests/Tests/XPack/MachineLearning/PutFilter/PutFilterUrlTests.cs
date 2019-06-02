using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.PutFilter
{
	public class PutFilterUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await PUT("/_ml/filters/filter_id")
				.Fluent(c => c.MachineLearning.PutFilter("filter_id", p => p))
				.Request(c => c.MachineLearning.PutFilter(new PutFilterRequest("filter_id")))
				.FluentAsync(c => c.MachineLearning.PutFilterAsync("filter_id", p => p))
				.RequestAsync(c => c.MachineLearning.PutFilterAsync(new PutFilterRequest("filter_id")));
	}
}
