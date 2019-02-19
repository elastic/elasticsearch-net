using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework;
using static Nest.Infer;

namespace Tests.Search.SearchTemplate
{
	public class SearchTemplateUrlTests
	{
		[U] public async Task Urls()
		{
			var hardcoded = "hardcoded";

			await UrlTester.POST("/hardcoded/doc/_search/template")
					.Request(c => c.SearchTemplate<Project>(new SearchTemplateRequest(hardcoded)))
					.Request(c => c.SearchTemplate<Project>(new SearchTemplateRequest<Project>(hardcoded)))
					.FluentAsync(c => c.SearchTemplateAsync<Project>(s => s.Index(hardcoded)))
					.RequestAsync(c => c.SearchTemplateAsync<Project>(new SearchTemplateRequest<Project>(hardcoded)))
					.FluentAsync(c => c.SearchTemplateAsync<Project>(s => s.Index(hardcoded)))
				;

			await UrlTester.POST("/project/_search/template")
				.Request(c => c.SearchTemplate<Project>(new SearchTemplateRequest("project")))
				.Fluent(c => c.SearchTemplate<Project>(s => s.Index("project")))
				.Request(c => c.SearchTemplate<Project>(new SearchTemplateRequest<Project>(typeof(Project))))
				.FluentAsync(c => c.SearchTemplateAsync<Project>(s => s.Index("project")))
				.RequestAsync(c => c.SearchTemplateAsync<Project>(new SearchTemplateRequest<Project>(typeof(Project))));

			await UrlTester.POST("/_search/template")
					.Fluent(c => c.SearchTemplate<Project>(s => s.AllIndices()))
					.Request(c => c.SearchTemplate<Project>(new SearchTemplateRequest()))
					.Request(c => c.SearchTemplate<Project>(new SearchTemplateRequest<Project>(Nest.Indices.All)))
					.FluentAsync(c => c.SearchTemplateAsync<Project>(s => s.AllIndices()))
					.RequestAsync(c => c.SearchTemplateAsync<Project>(new SearchTemplateRequest<Project>(Nest.Indices.All)))
					.RequestAsync(c => c.SearchTemplateAsync<Project>(new SearchTemplateRequest()))
				;
		}
	}
}
