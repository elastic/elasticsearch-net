using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.Cluster.ClusterReroute
{
	public class ClusterRerouteUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_cluster/reroute")
			.Fluent(c => c.Cluster.Reroute(r => r))
			.Request(c => c.Cluster.Reroute(new ClusterRerouteRequest()))
			.FluentAsync(c => c.Cluster.RerouteAsync(r => r))
			.RequestAsync(c => c.Cluster.RerouteAsync(new ClusterRerouteRequest()));
	}
}
