using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Search.SearchTemplate.GetSearchTemplate
{
	public class GetSearchTemplateUrlTests
	{
		[U] public async Task Urls()
		{
			var id = "the-id";
			await GET("/_search/template/the-id")
				.Fluent(c => c.GetSearchTemplate(id))
				.Request(c=>c.GetSearchTemplate(new GetSearchTemplateRequest(id)))
				.FluentAsync(c => c.GetSearchTemplateAsync(id))
				.RequestAsync(c=>c.GetSearchTemplateAsync(new GetSearchTemplateRequest(id)))
				;
		}
	}
}
