using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.IndexSettings.IndexTemplates.DeleteIndexTemplate
{
	public class DeleteTemplateUrlTests
	{
		[U] public async Task Urls()
		{
			var name = "temp";
			await DELETE($"/_template/{name}")
				.Fluent(c => c.DeleteIndexTemplate(name))
				.Request(c => c.DeleteIndexTemplate(new DeleteIndexTemplateRequest(name)))
				.FluentAsync(c => c.DeleteIndexTemplateAsync(name))
				.RequestAsync(c => c.DeleteIndexTemplateAsync(new DeleteIndexTemplateRequest(name)))
				;
		}
	}
}
