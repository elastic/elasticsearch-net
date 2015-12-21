using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.Cluster.ClusterHealth
{
	public class ClusterHealthUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_cluster/health")
				.Fluent(c => c.ClusterHealth())
				.Request(c => c.ClusterHealth(new ClusterHealthRequest()))
				.FluentAsync(c => c.ClusterHealthAsync())
				.RequestAsync(c => c.ClusterHealthAsync(new ClusterHealthRequest()))
				;

			await GET("/_cluster/health/project")
				.Fluent(c => c.ClusterHealth(h => h.Index<Project>()))
				.Request(c => c.ClusterHealth(new ClusterHealthRequest(typeof(Project))))
				.FluentAsync(c => c.ClusterHealthAsync(h => h.Index<Project>()))
				.RequestAsync(c => c.ClusterHealthAsync(new ClusterHealthRequest(typeof(Project))))
				;
		}
	}
}
