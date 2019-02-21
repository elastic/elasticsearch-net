using System;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Search.Search
{
	public class SearchUrlTests
	{
		[U] public async Task Urls()
		{
			var hardcoded = "hardcoded";
			await POST("/devs/_search")
					.Fluent(c => c.Search<Developer>())
					.Request(c => c.Search<Project>(new SearchRequest<Developer>()))
					.FluentAsync(c => c.SearchAsync<Developer>())
					.RequestAsync(c => c.SearchAsync<Project>(new SearchRequest<Developer>()))
				;

			await POST("/devs/_search")
					.Fluent(c => c.Search<Developer>(s => s))
					.Request(c => c.Search<Project>(new SearchRequest<Developer>(typeof(Developer))))
					.FluentAsync(c => c.SearchAsync<Developer>(s => s))
					.RequestAsync(c => c.SearchAsync<Project>(new SearchRequest<Developer>(typeof(Developer))))
				;

			await POST("/project/_search")
					.Fluent(c => c.Search<Project>(s => s))
					.Fluent(c => c.Search<Project>(s => s))
					.Request(c => c.Search<Project>(new SearchRequest("project")))
					.Request(c => c.Search<Project>(new SearchRequest<Project>("project")))
					.FluentAsync(c => c.SearchAsync<Project>(s => s))
					.RequestAsync(c => c.SearchAsync<Project>(new SearchRequest<Project>(typeof(Project))))
					.FluentAsync(c => c.SearchAsync<Project>(s => s))
				;

			await POST("/hardcoded/_search")
					.Fluent(c => c.Search<Project>(s => s.Index(hardcoded)))
					.Fluent(c => c.Search<Project>(s => s.Index(hardcoded)))
					.Request(c => c.Search<Project>(new SearchRequest(hardcoded)))
					.Request(c => c.Search<Project>(new SearchRequest<Project>(hardcoded)))
					.FluentAsync(c => c.SearchAsync<Project>(s => s.Index(hardcoded)))
					.RequestAsync(c => c.SearchAsync<Project>(new SearchRequest<Project>(hardcoded)))
					.FluentAsync(c => c.SearchAsync<Project>(s => s.Index(hardcoded)))
				;

			await POST("/_search")
					.Fluent(c => c.Search<Project>(s => s.AllIndices()))
					.Request(c => c.Search<Project>(new SearchRequest()))
					.Request(c => c.Search<Project>(new SearchRequest<Project>(Nest.Indices.All)))
					.FluentAsync(c => c.SearchAsync<Project>(s => s.AllIndices()))
					.RequestAsync(c => c.SearchAsync<Project>(new SearchRequest<Project>(Nest.Indices.All)))
					.RequestAsync(c => c.SearchAsync<Project>(new SearchRequest()))
				;

			await POST("/_search?scroll=1m")
					.Fluent(c => c.Search<Project>(s => s.AllIndices().Scroll(60000)))
					.Request(c => c.Search<Project>(new SearchRequest<Project>(Nest.Indices.All) { Scroll = TimeSpan.FromMinutes(1) }))
					.FluentAsync(c => c.SearchAsync<Project>(s => s.AllIndices().Scroll("1m")))
					.RequestAsync(c => c.SearchAsync<Project>(new SearchRequest<Project>(Nest.Indices.All) { Scroll = 60000 }))
				;
		}
	}
}
