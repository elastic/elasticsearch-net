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
			await POST("/_xpack/ml/filters/filter_id/_update")
				.Fluent(c => c.UpdateFilter("filter_id", p => p))
				.Request(c => c.UpdateFilter(new UpdateFilterRequest("filter_id")))
				.FluentAsync(c => c.UpdateFilterAsync("filter_id", p => p))
				.RequestAsync(c => c.UpdateFilterAsync(new UpdateFilterRequest("filter_id")));
	}
}
