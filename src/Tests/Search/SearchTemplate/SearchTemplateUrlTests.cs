using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Nest.Infer;

namespace Tests.Search.SearchTemplate
{
	public class SearchTemplateUrlTests
	{
		[U] public async Task Urls()
		{
			var hardcoded = "hardcoded";

			await UrlTester.POST("/hardcoded/doc/_search/template")
				.Request(c=>c.SearchTemplate<Project>(new SearchTemplateRequest(hardcoded, "doc")))
				.Request(c=>c.SearchTemplate<Project>(new SearchTemplateRequest<Project>(hardcoded, Type<Project>())))
				.FluentAsync(c=>c.SearchTemplateAsync<Project>(s=>s.Index(hardcoded).Type<Project>()))
				.RequestAsync(c=>c.SearchTemplateAsync<Project>(new SearchTemplateRequest<Project>(hardcoded, Type<Project>())))
				.FluentAsync(c=>c.SearchTemplateAsync<Project>(s=>s.Index(hardcoded).Type<Project>()))
				;

			await UrlTester.POST("/project/_search/template")
				.Request(c=>c.SearchTemplate<Project>(new SearchTemplateRequest("project")))
				.Fluent(c=>c.SearchTemplate<Project>(s=>s.Index("project").AllTypes()))
				.Request(c=>c.SearchTemplate<Project>(new SearchTemplateRequest<Project>(typeof(Project))))
				.FluentAsync(c=>c.SearchTemplateAsync<Project>(s=>s.Index("project").AllTypes()))
				.RequestAsync(c=>c.SearchTemplateAsync<Project>(new SearchTemplateRequest<Project>(typeof(Project))));

			await UrlTester.POST("/_search/template")
				.Fluent(c=>c.SearchTemplate<Project>(s=>s.AllIndices().AllTypes()))
				.Request(c=>c.SearchTemplate<Project>(new SearchTemplateRequest()))
				.Request(c=>c.SearchTemplate<Project>(new SearchTemplateRequest<Project>(Nest.Indices.All, Types.All)))
				.FluentAsync(c=>c.SearchTemplateAsync<Project>(s=>s.AllIndices().AllTypes()))
				.RequestAsync(c=>c.SearchTemplateAsync<Project>(new SearchTemplateRequest<Project>(Nest.Indices.All, Types.All)))
				.RequestAsync(c=>c.SearchTemplateAsync<Project>(new SearchTemplateRequest()))
				;
		}
	}
}
