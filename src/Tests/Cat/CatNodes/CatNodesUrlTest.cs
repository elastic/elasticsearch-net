using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Cat.CatNodes
{
	public class CatNodesUrlTest : IUrlTest
	{
		[U] public async Task Urls()
		{
			await GET("/_cat/nodes")
				.Fluent(c => c.CatNodes())
				.Request(c => c.CatNodes(new CatNodesRequest()))
				.FluentAsync(c => c.CatNodesAsync())
				.RequestAsync(c => c.CatNodesAsync(new CatNodesRequest()))
				;
		}
	}
}
