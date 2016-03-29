using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cluster.NodesHotThreads
{
	public class NodesHotThreadsUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_cluster/nodes/hotthreads")
				.Fluent(c => c.NodesHotThreads())
				.Request(c => c.NodesHotThreads(new NodesHotThreadsRequest()))
				.FluentAsync(c => c.NodesHotThreadsAsync())
				.RequestAsync(c => c.NodesHotThreadsAsync(new NodesHotThreadsRequest()))
				;

			await GET("/_cluster/nodes/foo/hotthreads")
				.Fluent(c => c.NodesHotThreads(n => n.NodeId("foo")))
				.Request(c => c.NodesHotThreads(new NodesHotThreadsRequest("foo")))
				.FluentAsync(c => c.NodesHotThreadsAsync(n => n.NodeId("foo")))
				.RequestAsync(c => c.NodesHotThreadsAsync(new NodesHotThreadsRequest("foo")))
				;
		}
	}
}
