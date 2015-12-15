using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Search.SearchTemplate.DeleteSearchTemplate
{
	public class DeleteSearchTemplateUrlTests
	{
		[U] public async Task Urls()
		{
			var id = "the-id";
			await DELETE("/_search/template/the-id")
				.Fluent(c => c.DeleteSearchTemplate(id))
				.Request(c=>c.DeleteSearchTemplate(new DeleteSearchTemplateRequest(id)))
				.FluentAsync(c => c.DeleteSearchTemplateAsync(id))
				.RequestAsync(c=>c.DeleteSearchTemplateAsync(new DeleteSearchTemplateRequest(id)))
				;
		}
	}
}
