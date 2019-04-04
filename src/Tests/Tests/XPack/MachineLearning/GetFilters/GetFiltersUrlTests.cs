using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.GetFilters
{
	public class GetFiltersUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_xpack/ml/filters")
				.Fluent(c => c.GetFilters(p => p))
				.Request(c => c.GetFilters(new GetFiltersRequest()))
				.FluentAsync(c => c.GetFiltersAsync(p => p))
				.RequestAsync(c => c.GetFiltersAsync(new GetFiltersRequest()));

			await GET("/_xpack/ml/filters/1")
				.Request(c => c.GetFilters(new GetFiltersRequest(1)))
				.Fluent(c => c.GetFilters(r => r.FilterId(1)))
				.FluentAsync(c => c.GetFiltersAsync(r => r.FilterId(1)))
				.RequestAsync(c => c.GetFiltersAsync(new GetFiltersRequest(1)));
		}
	}
}
