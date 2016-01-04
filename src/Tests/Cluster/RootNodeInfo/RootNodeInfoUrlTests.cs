using System.Threading.Tasks;
using Nest;
using Tests.Framework;
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
