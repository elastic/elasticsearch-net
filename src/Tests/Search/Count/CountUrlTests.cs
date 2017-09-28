using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Search.Count
{
	public class CountUrlTests
	{
		[U] public async Task Urls()
		{
			var hardcoded = "hardcoded";
			await GET("/commits/commits/_count")
				.Fluent(c=>c.Count<CommitActivity>())
				.Request(c=>c.Count<Project>(new CountRequest<CommitActivity>()))
				.FluentAsync(c=>c.CountAsync<CommitActivity>())
				.RequestAsync(c=>c.CountAsync<Project>(new CountRequest<CommitActivity>()))
				;

			await GET("/commits/commits/_count?q=querystring")
				.Fluent(c=>c.Count<CommitActivity>(s=>s.QueryOnQueryString("querystring")))
				.Request(c=>c.Count<Project>(new CountRequest<CommitActivity>() { QueryOnQueryString = "querystring" }))
				.FluentAsync(c=>c.CountAsync<CommitActivity>(s=>s.QueryOnQueryString("querystring")))
				.RequestAsync(c=>c.CountAsync<Project>(new CountRequest<CommitActivity>() { QueryOnQueryString = "querystring" }))
				;
			await GET("/commits/hardcoded/_count")
				.Fluent(c=>c.Count<CommitActivity>(s=>s.Type(hardcoded)))
				.Request(c=>c.Count<Project>(new CountRequest<CommitActivity>(typeof(CommitActivity), hardcoded)))
				.FluentAsync(c=>c.CountAsync<CommitActivity>(s=>s.Type(hardcoded)))
				.RequestAsync(c=>c.CountAsync<Project>(new CountRequest<CommitActivity>(typeof(CommitActivity), hardcoded)))
				;

			await GET("/project/_count")
				.Fluent(c=>c.Count<Project>(s=>s.Type(Types.All)))
				.Fluent(c=>c.Count<Project>(s=>s.AllTypes()))
				.Request(c=>c.Count<Project>(new CountRequest("project")))
				.Request(c=>c.Count<Project>(new CountRequest<Project>("project", Types.All)))
				.FluentAsync(c=>c.CountAsync<Project>(s=>s.Type(Types.All)))
				.RequestAsync(c=>c.CountAsync<Project>(new CountRequest<Project>(typeof(Project), Types.All)))
				.FluentAsync(c=>c.CountAsync<Project>(s=>s.AllTypes()))
				;

			await GET("/hardcoded/_count")
				.Fluent(c=>c.Count<Project>(s=>s.Index(hardcoded).Type(Types.All)))
				.Fluent(c=>c.Count<Project>(s=>s.Index(hardcoded).AllTypes()))
				.Request(c=>c.Count<Project>(new CountRequest(hardcoded)))
				.Request(c=>c.Count<Project>(new CountRequest<Project>(hardcoded, Types.All)))
				.FluentAsync(c=>c.CountAsync<Project>(s=>s.Index(hardcoded).Type(Types.All)))
				.RequestAsync(c=>c.CountAsync<Project>(new CountRequest<Project>(hardcoded, Types.All)))
				.FluentAsync(c=>c.CountAsync<Project>(s=>s.Index(hardcoded).AllTypes()))
				;

			await GET("/_count")
				.Fluent(c=>c.Count<Project>(s=>s.AllTypes().AllIndices()))
				.Request(c=>c.Count<Project>(new CountRequest()))
				.Request(c=>c.Count<Project>(new CountRequest<Project>(Nest.Indices.All, Types.All)))
				.FluentAsync(c=>c.CountAsync<Project>(s=>s.AllIndices().Type(Types.All)))
				.RequestAsync(c=>c.CountAsync<Project>(new CountRequest<Project>(Nest.Indices.All, Types.All)))
				.RequestAsync(c=>c.CountAsync<Project>(new CountRequest()))
				;

			await POST("/_count")
				.Fluent(c=>c.Count<Project>(s=>s.AllTypes().AllIndices().Query(q=>q.MatchAll())))
				.Request(c=>c.Count<Project>(new CountRequest() { Query = new MatchAllQuery() }))
				.Request(c=>c.Count<Project>(new CountRequest<Project>(Nest.Indices.All, Types.All) { Query = new MatchAllQuery() }))
				.FluentAsync(c=>c.CountAsync<Project>(s=>s.AllIndices().Type(Types.All).Query(q=>q.MatchAll())))
				.RequestAsync(c=>c.CountAsync<Project>(new CountRequest<Project>(Nest.Indices.All, Types.All) { Query = new MatchAllQuery() }))
				.RequestAsync(c=>c.CountAsync<Project>(new CountRequest() { Query = new MatchAllQuery() }))
				;
		}
	}
}
