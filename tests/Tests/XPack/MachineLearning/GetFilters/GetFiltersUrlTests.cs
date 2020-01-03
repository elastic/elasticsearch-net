using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.GetFilters
{
	public class GetFiltersUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_ml/filters")
				.Fluent(c => c.MachineLearning.GetFilters(p => p))
				.Request(c => c.MachineLearning.GetFilters(new GetFiltersRequest()))
				.FluentAsync(c => c.MachineLearning.GetFiltersAsync(p => p))
				.RequestAsync(c => c.MachineLearning.GetFiltersAsync(new GetFiltersRequest()));

			await GET("/_ml/filters/1")
				.Request(c => c.MachineLearning.GetFilters(new GetFiltersRequest(1)))
				.Fluent(c => c.MachineLearning.GetFilters(r => r.FilterId(1)))
				.FluentAsync(c => c.MachineLearning.GetFiltersAsync(r => r.FilterId(1)))
				.RequestAsync(c => c.MachineLearning.GetFiltersAsync(new GetFiltersRequest(1)));
		}
	}
}
