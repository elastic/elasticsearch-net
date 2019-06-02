using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.XPack.CrossClusterReplication.Follow.FollowIndexStats
{
	public class FollowIndexStatsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			var name = "x";
			await UrlTester.GET($"/{name}/_ccr/stats")
				.Fluent(c => c.CrossClusterReplication.FollowIndexStats(name, d => d))
				.Request(c => c.CrossClusterReplication.FollowIndexStats(new FollowIndexStatsRequest(name)))
				.FluentAsync(c => c.CrossClusterReplication.FollowIndexStatsAsync(name, d => d))
				.RequestAsync(c => c.CrossClusterReplication.FollowIndexStatsAsync(new FollowIndexStatsRequest(name)));

		}
	}
}
