using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.UpdateFilter
{
	public class UpdateFilterUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await POST("/_ml/filters/filter_id/_update")
				.Fluent(c => c.MachineLearning.UpdateFilter("filter_id", p => p))
				.Request(c => c.MachineLearning.UpdateFilter(new UpdateFilterRequest("filter_id")))
				.FluentAsync(c => c.MachineLearning.UpdateFilterAsync("filter_id", p => p))
				.RequestAsync(c => c.MachineLearning.UpdateFilterAsync(new UpdateFilterRequest("filter_id")));
	}
}
