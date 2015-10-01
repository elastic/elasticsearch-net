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
	public class NodesInfoUrlTest : IUrlTest
	{
		[U] public async Task Urls()
		{
			await GET("/_nodes")
				.Fluent(c => c.NodesInfo())
				.Request(c => c.NodesInfo(new NodesInfoRequest()))
				.FluentAsync(c => c.NodesInfoAsync())
				.RequestAsync(c => c.NodesInfoAsync(new NodesInfoRequest()))
				;

			await GET("/_nodes/foo")
				.Fluent(c => c.NodesInfo(n => n.NodeId("foo")))
				.Request(c => c.NodesInfo(new NodesInfoRequest("foo")))
				.FluentAsync(c => c.NodesInfoAsync(n => n.NodeId("foo")))
				.RequestAsync(c => c.NodesInfoAsync(new NodesInfoRequest("foo")))
				;

			// TODO: need to implement Metric
			await GET("/_nodes/{metric}")
				.Fluent(c => c.NodesInfo())
				.Request(c => c.NodesInfo(new NodesInfoRequest()))
				.FluentAsync(c => c.NodesInfoAsync())
				.RequestAsync(c => c.NodesInfoAsync(new NodesInfoRequest()))
				;

			// TODO: need to implement Metric
			await GET("/_nodes/foo/{metric}")
				.Fluent(c => c.NodesInfo(n => n.NodeId("foo")))
				.Request(c => c.NodesInfo(new NodesInfoRequest("foo")))
				.FluentAsync(c => c.NodesInfoAsync(n => n.NodeId("foo")))
				.RequestAsync(c => c.NodesInfoAsync(new NodesInfoRequest("foo")))
				;
		}
	}
}
