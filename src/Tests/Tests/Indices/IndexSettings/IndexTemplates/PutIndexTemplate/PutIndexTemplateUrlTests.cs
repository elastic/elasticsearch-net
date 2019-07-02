using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.IndexSettings.IndexTemplates.PutIndexTemplate
{
	public class PutTemplateUrlTests
	{
		[U] public async Task Urls()
		{
			var name = "temp";
			await PUT($"/_template/{name}")
					.Fluent(c => c.Indices.PutTemplate(name, p => p))
					.Request(c => c.Indices.PutTemplate(new PutIndexTemplateRequest(name)))
					.FluentAsync(c => c.Indices.PutTemplateAsync(name, p => p))
					.RequestAsync(c => c.Indices.PutTemplateAsync(new PutIndexTemplateRequest(name)))
				;
		}
	}
}
