using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Search.Count
{
	public class CountUrlTests
	{
		[U] public async Task Urls()
		{
			var hardcoded = "hardcoded";
			await GET("/devs/developer/_count")
					.Fluent(c => c.Count<Developer>())
					.Request(c => c.Count<Project>(new CountRequest<Developer>()))
					.FluentAsync(c => c.CountAsync<Developer>())
					.RequestAsync(c => c.CountAsync<Project>(new CountRequest<Developer>()))
				;

			await GET("/devs/developer/_count?q=querystring")
					.Fluent(c => c.Count<Developer>(s => s.QueryOnQueryString("querystring")))
					.Request(c => c.Count<Project>(new CountRequest<Developer>() { QueryOnQueryString = "querystring" }))
					.FluentAsync(c => c.CountAsync<Developer>(s => s.QueryOnQueryString("querystring")))
					.RequestAsync(c => c.CountAsync<Project>(new CountRequest<Developer>() { QueryOnQueryString = "querystring" }))
				;
			await GET($"/devs/{hardcoded}/_count")
					.Fluent(c => c.Count<Developer>(s => s))
					.Request(c => c.Count<Project>(new CountRequest<Developer>()))
					.FluentAsync(c => c.CountAsync<Developer>(s => s))
					.RequestAsync(c => c.CountAsync<Project>(new CountRequest<Developer>()))
				;

			await GET("/project/_count")
					.Fluent(c => c.Count<Project>(s => s))
					.Fluent(c => c.Count<Project>(s => s))
					.Request(c => c.Count<Project>(new CountRequest("project")))
					.Request(c => c.Count<Project>(new CountRequest<Project>("project")))
					.FluentAsync(c => c.CountAsync<Project>(s => s))
					.RequestAsync(c => c.CountAsync<Project>(new CountRequest<Project>(typeof(Project))))
					.FluentAsync(c => c.CountAsync<Project>(s => s))
				;

			await GET($"/{hardcoded}/_count")
					.Fluent(c => c.Count<Project>(s => s.Index(hardcoded)))
					.Fluent(c => c.Count<Project>(s => s.Index(hardcoded)))
					.Request(c => c.Count<Project>(new CountRequest(hardcoded)))
					.Request(c => c.Count<Project>(new CountRequest<Project>(hardcoded)))
					.FluentAsync(c => c.CountAsync<Project>(s => s.Index(hardcoded)))
					.RequestAsync(c => c.CountAsync<Project>(new CountRequest<Project>(hardcoded)))
					.FluentAsync(c => c.CountAsync<Project>(s => s.Index(hardcoded)))
				;

			await GET("/_count")
					.Fluent(c => c.Count<Project>(s => s.AllIndices()))
					.Request(c => c.Count<Project>(new CountRequest()))
					.Request(c => c.Count<Project>(new CountRequest<Project>(Nest.Indices.All)))
					.FluentAsync(c => c.CountAsync<Project>(s => s.AllIndices()))
					.RequestAsync(c => c.CountAsync<Project>(new CountRequest<Project>(Nest.Indices.All)))
					.RequestAsync(c => c.CountAsync<Project>(new CountRequest()))
				;

			await POST("/_count")
					.Fluent(c => c.Count<Project>(s => s.AllIndices().Query(q => q.MatchAll())))
					.Request(c => c.Count<Project>(new CountRequest() { Query = new MatchAllQuery() }))
					.Request(c => c.Count<Project>(new CountRequest<Project>(Nest.Indices.All) { Query = new MatchAllQuery() }))
					.FluentAsync(c => c.CountAsync<Project>(s => s.AllIndices().Query(q => q.MatchAll())))
					.RequestAsync(c => c.CountAsync<Project>(new CountRequest<Project>(Nest.Indices.All) { Query = new MatchAllQuery() }))
					.RequestAsync(c => c.CountAsync<Project>(new CountRequest() { Query = new MatchAllQuery() }))
				;
		}
	}
}
