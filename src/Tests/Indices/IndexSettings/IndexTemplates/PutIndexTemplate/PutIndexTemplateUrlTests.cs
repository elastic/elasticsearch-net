using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.IndexSettings.IndexTemplates.PutIndexTemplate
{
	public class PutTemplateUrlTests
	{
		[U] public async Task Urls()
		{
			var name = "temp";
			await PUT($"/_template/{name}")
				.Fluent(c => c.PutIndexTemplate(name, p=>p))
				.Request(c => c.PutIndexTemplate(new PutIndexTemplateRequest(name)))
				.FluentAsync(c => c.PutIndexTemplateAsync(name, p=>p))
				.RequestAsync(c => c.PutIndexTemplateAsync(new PutIndexTemplateRequest(name)))
				;
		}
	}
}
