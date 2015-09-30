using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatAliases
{
	public class CatPluginsUrlTest : IUrlTest
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
