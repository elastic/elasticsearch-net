using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;

namespace Tests.XPack.CrossClusterReplication.Follow.FollowIndexStats
{
	public class FollowIndexStatsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			var name = "x";
			await UrlTester.GET($"/{name}/_ccr/stats")
				.Fluent(c => c.FollowIndexStats(name, d => d))
				.Request(c => c.FollowIndexStats(new FollowIndexStatsRequest(name)))
				.FluentAsync(c => c.FollowIndexStatsAsync(name, d => d))
				.RequestAsync(c => c.FollowIndexStatsAsync(new FollowIndexStatsRequest(name)));

		}
	}
}
