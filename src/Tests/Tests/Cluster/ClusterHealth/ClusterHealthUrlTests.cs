using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cluster.ClusterHealth
{
	public class ClusterHealthUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
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
