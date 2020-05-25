using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.DeleteFilter
{
	public class DeleteFilterUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() =>
			await DELETE("/_xpack/ml/filters/filter_id")
				.Fluent(c => c.DeleteFilter("filter_id", p => p))
				.Request(c => c.DeleteFilter(new DeleteFilterRequest("filter_id")))
				.FluentAsync(c => c.DeleteFilterAsync("filter_id", p => p))
				.RequestAsync(c => c.DeleteFilterAsync(new DeleteFilterRequest("filter_id")));
	}
}
