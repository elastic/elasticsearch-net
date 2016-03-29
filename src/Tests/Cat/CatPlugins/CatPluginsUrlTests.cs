using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatPlugins
{
	public class CatPluginsUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_cat/plugins")
				.Fluent(c => c.CatPlugins())
				.Request(c => c.CatPlugins(new CatPluginsRequest()))
				.FluentAsync(c => c.CatPluginsAsync())
				.RequestAsync(c => c.CatPluginsAsync(new CatPluginsRequest()))
				;
		}
	}
}
