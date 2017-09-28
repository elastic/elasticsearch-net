using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;
using System;

namespace Tests.Search.Search
{
	public class SearchUrlTests
	{
		[U] public async Task Urls()
		{
			var hardcoded = "hardcoded";
			await POST("/commits/commits/_search")
				.Fluent(c=>c.Search<CommitActivity>())
				.Request(c=>c.Search<Project>(new SearchRequest<CommitActivity>()))
				.FluentAsync(c=>c.SearchAsync<CommitActivity>())
				.RequestAsync(c=>c.SearchAsync<Project>(new SearchRequest<CommitActivity>()))
				;

			await POST("/commits/hardcoded/_search")
				.Fluent(c=>c.Search<CommitActivity>(s=>s.Type(hardcoded)))
				.Request(c=>c.Search<Project>(new SearchRequest<CommitActivity>(typeof(CommitActivity), hardcoded)))
				.FluentAsync(c=>c.SearchAsync<CommitActivity>(s=>s.Type(hardcoded)))
				.RequestAsync(c=>c.SearchAsync<Project>(new SearchRequest<CommitActivity>(typeof(CommitActivity), hardcoded)))
				;

			await POST("/project/_search")
				.Fluent(c=>c.Search<Project>(s=>s.Type(Types.All)))
				.Fluent(c=>c.Search<Project>(s=>s.AllTypes()))
				.Request(c=>c.Search<Project>(new SearchRequest("project")))
				.Request(c=>c.Search<Project>(new SearchRequest<Project>("project", Types.All)))
				.FluentAsync(c=>c.SearchAsync<Project>(s=>s.Type(Types.All)))
				.RequestAsync(c=>c.SearchAsync<Project>(new SearchRequest<Project>(typeof(Project), Types.All)))
				.FluentAsync(c=>c.SearchAsync<Project>(s=>s.AllTypes()))
				;

			await POST("/hardcoded/_search")
				.Fluent(c=>c.Search<Project>(s=>s.Index(hardcoded).Type(Types.All)))
				.Fluent(c=>c.Search<Project>(s=>s.Index(hardcoded).AllTypes()))
				.Request(c=>c.Search<Project>(new SearchRequest(hardcoded)))
				.Request(c=>c.Search<Project>(new SearchRequest<Project>(hardcoded, Types.All)))
				.FluentAsync(c=>c.SearchAsync<Project>(s=>s.Index(hardcoded).Type(Types.All)))
				.RequestAsync(c=>c.SearchAsync<Project>(new SearchRequest<Project>(hardcoded, Types.All)))
				.FluentAsync(c=>c.SearchAsync<Project>(s=>s.Index(hardcoded).AllTypes()))
				;

			await POST("/_search")
				.Fluent(c=>c.Search<Project>(s=>s.AllTypes().AllIndices()))
				.Request(c=>c.Search<Project>(new SearchRequest()))
				.Request(c=>c.Search<Project>(new SearchRequest<Project>(Nest.Indices.All, Types.All)))
				.FluentAsync(c=>c.SearchAsync<Project>(s=>s.AllIndices().Type(Types.All)))
				.RequestAsync(c=>c.SearchAsync<Project>(new SearchRequest<Project>(Nest.Indices.All, Types.All)))
				.RequestAsync(c=>c.SearchAsync<Project>(new SearchRequest()))
				;

			await POST("/_search?scroll=1m")
				.Fluent(c=>c.Search<Project>(s=>s.AllTypes().AllIndices().Scroll(60000)))
				.Request(c=>c.Search<Project>(new SearchRequest<Project>(Nest.Indices.All, Types.All) { Scroll = TimeSpan.FromMinutes(1) }))
				.FluentAsync(c=>c.SearchAsync<Project>(s=>s.AllIndices().Type(Types.All).Scroll("1m")))
				.RequestAsync(c=>c.SearchAsync<Project>(new SearchRequest<Project>(Nest.Indices.All, Types.All) { Scroll = 60000 } ))
				;
		}
	}
}
