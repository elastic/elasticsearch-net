using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.AsyncSearch.Get
{
	public class AsyncSearchGetUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_async_search/search_id")
			.Fluent(c => c.AsyncSearch.Get<Project>("search_id", f => f))
			.Request(c => c.AsyncSearch.Get<Project>(new AsyncSearchGetRequest("search_id")))
			.FluentAsync(c => c.AsyncSearch.GetAsync<Project>("search_id", f => f))
			.RequestAsync(c => c.AsyncSearch.GetAsync<Project>(new AsyncSearchGetRequest("search_id")));
	}
}
