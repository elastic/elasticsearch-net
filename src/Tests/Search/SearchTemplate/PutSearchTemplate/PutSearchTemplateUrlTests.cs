using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Search.SearchTemplate.PutSearchTemplate
{
	public class PutSearchTemplateUrlTests
	{
		[U] public async Task Urls()
		{
			var id = "the-id";
			await PUT("/_search/template/the-id")
				.Fluent(c => c.PutSearchTemplate(id, s => s.Template("{}")))
				.Request(c=>c.PutSearchTemplate(new PutSearchTemplateRequest(id) { Template = "{}" }))
				.FluentAsync(c => c.PutSearchTemplateAsync(id, s => s.Template("{}")))
				.RequestAsync(c=>c.PutSearchTemplateAsync(new PutSearchTemplateRequest(id) { Template = "{}" }))
				;
		}
	}
}
