using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Cluster.RootNodeInfo
{
	public class RootNodeInfoUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/")
				.Fluent(c => c.RootNodeInfo())
				.Request(c => c.RootNodeInfo(new RootNodeInfoRequest()))
				.FluentAsync(c => c.RootNodeInfoAsync())
				.RequestAsync(c => c.RootNodeInfoAsync(new RootNodeInfoRequest()))
				;
		}
	}
}
