using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.AsyncSearch.Status
{
	public class AsyncSearchStatusUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_async_search/status/search_id")
			.Fluent(c => c.AsyncSearch.Status("search_id", f => f))
			.Request(c => c.AsyncSearch.Status(new AsyncSearchStatusRequest("search_id")))
			.FluentAsync(c => c.AsyncSearch.StatusAsync("search_id", f => f))
			.RequestAsync(c => c.AsyncSearch.StatusAsync(new AsyncSearchStatusRequest("search_id")));
	}
}
