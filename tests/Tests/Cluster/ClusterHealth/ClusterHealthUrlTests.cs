using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;
using static Nest.Infer;

namespace Tests.Cluster.ClusterHealth
{
	public class ClusterHealthUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_cluster/health")
					.Fluent(c => c.Cluster.Health())
					.Request(c => c.Cluster.Health(new ClusterHealthRequest()))
					.FluentAsync(c => c.Cluster.HealthAsync())
					.RequestAsync(c => c.Cluster.HealthAsync(new ClusterHealthRequest()))
				;

			await GET("/_cluster/health/_all")
				.Fluent(c => c.Cluster.Health(AllIndices))
				.Request(c => c.Cluster.Health(new ClusterHealthRequest(AllIndices)))
				.FluentAsync(c => c.Cluster.HealthAsync(AllIndices))
				.RequestAsync(c => c.Cluster.HealthAsync(new ClusterHealthRequest(AllIndices)));

			await GET("/_cluster/health/project")
					.Fluent(c => c.Cluster.Health(Index<Project>()))
					.Request(c => c.Cluster.Health(new ClusterHealthRequest(typeof(Project))))
					.FluentAsync(c => c.Cluster.HealthAsync(Index<Project>()))
					.RequestAsync(c => c.Cluster.HealthAsync(new ClusterHealthRequest(typeof(Project))))
				;
		}
	}
}
