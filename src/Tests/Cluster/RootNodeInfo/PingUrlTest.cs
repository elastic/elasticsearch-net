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
	public class RootNodeInfoUrlTest : IUrlTest
	{
		[U] public async Task Urls()
		{
			await GET("/")
				.Fluent(c => c.RootNodeInfo())
				.Request(c => c.RootNodeInfo(new InfoRequest()))
				.FluentAsync(c => c.RootNodeInfoAsync())
				.RequestAsync(c => c.RootNodeInfoAsync(new InfoRequest()))
				;
		}
	}
}
