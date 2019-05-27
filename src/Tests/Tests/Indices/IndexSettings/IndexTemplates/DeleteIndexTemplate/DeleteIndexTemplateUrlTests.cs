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
					.Fluent(c => c.Indices.DeleteTemplate(name))
					.Request(c => c.Indices.DeleteTemplate(new DeleteIndexTemplateRequest(name)))
					.FluentAsync(c => c.Indices.DeleteTemplateAsync(name))
					.RequestAsync(c => c.Indices.DeleteTemplateAsync(new DeleteIndexTemplateRequest(name)))
				;
		}
	}
}
