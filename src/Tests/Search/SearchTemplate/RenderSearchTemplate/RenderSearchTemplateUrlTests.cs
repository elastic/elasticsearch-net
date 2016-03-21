using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Search.SearchTemplate.RenderSearchTemplate
{
	public class RenderSearchTemplateUrlTests
	{
		[U] public async Task Urls()
		{
			var id = "the-id";
			await POST("/_render/template/the-id")
				.Fluent(c => c.RenderSearchTemplate(s=>s.Id(id)))
				.Request(c=>c.RenderSearchTemplate(new RenderSearchTemplateRequest(id)))
				.FluentAsync(c => c.RenderSearchTemplateAsync(s=>s.Id(id)))
				.RequestAsync(c=>c.RenderSearchTemplateAsync(new RenderSearchTemplateRequest(id)))
				;

			await POST("/_render/template")
				.Fluent(c => c.RenderSearchTemplate(s=>s.Inline("")))
				.Request(c=>c.RenderSearchTemplate(new RenderSearchTemplateRequest { Inline = "" }))
				.FluentAsync(c => c.RenderSearchTemplateAsync(s=>s.Inline("")))
				.RequestAsync(c=>c.RenderSearchTemplateAsync(new RenderSearchTemplateRequest { Inline = "" }))
				;
		}
	}
}
