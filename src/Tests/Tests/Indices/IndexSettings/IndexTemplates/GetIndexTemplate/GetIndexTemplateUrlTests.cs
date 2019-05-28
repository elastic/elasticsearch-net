using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Indices.IndexSettings.IndexTemplates.GetIndexTemplate
{
	public class GetTemplateUrlTests
	{
		[U] public async Task Urls()
		{
			var name = "temp";
			await GET($"/_template/{name}")
					.Fluent(c => c.Indices.GetIndexTemplate(name))
					.Request(c => c.Indices.GetIndexTemplate(new GetIndexTemplateRequest(name)))
					.FluentAsync(c => c.Indices.GetIndexTemplateAsync(name))
					.RequestAsync(c => c.Indices.GetIndexTemplateAsync(new GetIndexTemplateRequest(name)))
				;

			await GET($"/_template")
					.Fluent(c => c.Indices.GetIndexTemplate())
					.Request(c => c.Indices.GetIndexTemplate(new GetIndexTemplateRequest()))
					.FluentAsync(c => c.Indices.GetIndexTemplateAsync())
					.RequestAsync(c => c.Indices.GetIndexTemplateAsync(new GetIndexTemplateRequest()))
				;
		}
	}
}
