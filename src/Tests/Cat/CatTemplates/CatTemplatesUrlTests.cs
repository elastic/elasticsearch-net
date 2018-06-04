using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatTemplates
{
	public class CatTemplatesUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_cat/templates")
				.Fluent(c => c.CatTemplates())
				.Request(c => c.CatTemplates(new CatTemplatesRequest()))
				.FluentAsync(c => c.CatTemplatesAsync())
				.RequestAsync(c => c.CatTemplatesAsync(new CatTemplatesRequest()))
				;

			await GET("/_cat/templates/index-%2A")
				.Fluent(c => c.CatTemplates(r => r.Name("index-*")))
				.Request(c => c.CatTemplates(new CatTemplatesRequest("index-*")))
				.FluentAsync(c => c.CatTemplatesAsync(r => r.Name("index-*")))
				.RequestAsync(c => c.CatTemplatesAsync(new CatTemplatesRequest("index-*")));
		}
	}
}
