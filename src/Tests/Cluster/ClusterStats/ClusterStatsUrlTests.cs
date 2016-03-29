using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cluster.ClusterStats
{
	public class ClusterStatsUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_cluster/stats")
				.Fluent(c => c.ClusterStats())
				.Request(c => c.ClusterStats(new ClusterStatsRequest()))
				.FluentAsync(c => c.ClusterStatsAsync())
				.RequestAsync(c => c.ClusterStatsAsync(new ClusterStatsRequest()))
				;

			await GET("/_cluster/stats/nodes/foo")
				.Fluent(c => c.ClusterStats(s => s.NodeId("foo")))
				.Request(c => c.ClusterStats(new ClusterStatsRequest("foo")))
				.FluentAsync(c => c.ClusterStatsAsync(s => s.NodeId("foo")))
				.RequestAsync(c => c.ClusterStatsAsync(new ClusterStatsRequest("foo")))
				;
		}
	}
}
