using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.DeleteFilter
{
	public class DeleteFilterUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await DELETE("/_ml/filters/filter_id")
				.Fluent(c => c.MachineLearning.DeleteFilter("filter_id", p => p))
				.Request(c => c.MachineLearning.DeleteFilter(new DeleteFilterRequest("filter_id")))
				.FluentAsync(c => c.MachineLearning.DeleteFilterAsync("filter_id", p => p))
				.RequestAsync(c => c.MachineLearning.DeleteFilterAsync(new DeleteFilterRequest("filter_id")));
	}
}
