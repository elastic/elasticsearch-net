using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
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
					.Fluent(c => c.Indices.DeleteIndexTemplate(name))
					.Request(c => c.Indices.DeleteIndexTemplate(new DeleteIndexTemplateRequest(name)))
					.FluentAsync(c => c.Indices.DeleteIndexTemplateAsync(name))
					.RequestAsync(c => c.Indices.DeleteIndexTemplateAsync(new DeleteIndexTemplateRequest(name)))
				;
		}
	}
}
