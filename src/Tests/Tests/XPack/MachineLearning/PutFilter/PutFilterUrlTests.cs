using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.PutFilter
{
	public class PutFilterUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await PUT("/_xpack/ml/filters/filter_id")
				.Fluent(c => c.PutFilter("filter_id", p => p))
				.Request(c => c.PutFilter(new PutFilterRequest("filter_id")))
				.FluentAsync(c => c.PutFilterAsync("filter_id", p => p))
				.RequestAsync(c => c.PutFilterAsync(new PutFilterRequest("filter_id")));
	}
}
