using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.AsyncSearch.Submit
{
	public class AsyncSearchSubmitUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			var hardcoded = "hardcoded";
			await POST("/devs/_async_search")
					.Fluent(c => c.AsyncSearch.Submit<Developer>())
					.Request(c => c.AsyncSearch.Submit<Project>(new AsyncSearchSubmitRequest<Developer>()))
					.FluentAsync(c => c.AsyncSearch.SubmitAsync<Developer>())
					.RequestAsync(c => c.AsyncSearch.SubmitAsync<Project>(new AsyncSearchSubmitRequest<Developer>()))
				;

			await POST("/devs/_async_search")
					.Fluent(c => c.AsyncSearch.Submit<Developer>(s => s))
					.Request(c => c.AsyncSearch.Submit<Project>(new AsyncSearchSubmitRequest<Developer>(typeof(Developer))))
					.FluentAsync(c => c.AsyncSearch.SubmitAsync<Developer>(s => s))
					.RequestAsync(c => c.AsyncSearch.SubmitAsync<Project>(new AsyncSearchSubmitRequest<Developer>(typeof(Developer))))
				;

			await POST("/project/_async_search")
					.Fluent(c => c.AsyncSearch.Submit<Project>(s => s))
					.Fluent(c => c.AsyncSearch.Submit<Project>(s => s))
					.Request(c => c.AsyncSearch.Submit<Project>(new AsyncSearchSubmitRequest("project")))
					.Request(c => c.AsyncSearch.Submit<Project>(new AsyncSearchSubmitRequest<Project>("project")))
					.FluentAsync(c => c.AsyncSearch.SubmitAsync<Project>(s => s))
					.RequestAsync(c => c.AsyncSearch.SubmitAsync<Project>(new AsyncSearchSubmitRequest<Project>(typeof(Project))))
					.FluentAsync(c => c.AsyncSearch.SubmitAsync<Project>(s => s))
				;

			await POST("/hardcoded/_async_search")
					.Fluent(c => c.AsyncSearch.Submit<Project>(s => s.Index(hardcoded)))
					.Fluent(c => c.AsyncSearch.Submit<Project>(s => s.Index(hardcoded)))
					.Request(c => c.AsyncSearch.Submit<Project>(new AsyncSearchSubmitRequest(hardcoded)))
					.Request(c => c.AsyncSearch.Submit<Project>(new AsyncSearchSubmitRequest<Project>(hardcoded)))
					.FluentAsync(c => c.AsyncSearch.SubmitAsync<Project>(s => s.Index(hardcoded)))
					.RequestAsync(c => c.AsyncSearch.SubmitAsync<Project>(new AsyncSearchSubmitRequest<Project>(hardcoded)))
					.FluentAsync(c => c.AsyncSearch.SubmitAsync<Project>(s => s.Index(hardcoded)))
				;

			await POST("/_all/_async_search")
					.Fluent(c => c.AsyncSearch.Submit<Project>(s => s.AllIndices()))
					.Request(c => c.AsyncSearch.Submit<Project>(new AsyncSearchSubmitRequest<Project>(Nest.Indices.All)))
					.FluentAsync(c => c.AsyncSearch.SubmitAsync<Project>(s => s.AllIndices()))
					.RequestAsync(c => c.AsyncSearch.SubmitAsync<Project>(new AsyncSearchSubmitRequest<Project>(Nest.Indices.All)))
				;
		}
	}
}
