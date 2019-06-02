using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.XPack.CrossClusterReplication.Follow.PauseFollowIndex
{
	public class PauseFollowIndexUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			var name = "x";
			await UrlTester.POST($"/{name}/_ccr/pause_follow")
				.Fluent(c => c.CrossClusterReplication.PauseFollowIndex(name, d => d))
				.Request(c => c.CrossClusterReplication.PauseFollowIndex(new PauseFollowIndexRequest(name)))
				.FluentAsync(c => c.CrossClusterReplication.PauseFollowIndexAsync(name, d => d))
				.RequestAsync(c => c.CrossClusterReplication.PauseFollowIndexAsync(new PauseFollowIndexRequest(name)));

		}
	}
}
