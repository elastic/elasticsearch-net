using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;

namespace Tests.XPack.CrossClusterReplication.Follow.PauseFollowIndex
{
	public class PauseFollowIndexUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			var name = "x";
			await UrlTester.POST($"/{name}/_ccr/pause_follow")
				.Fluent(c => c.PauseFollowIndex(name, d => d))
				.Request(c => c.PauseFollowIndex(new PauseFollowIndexRequest(name)))
				.FluentAsync(c => c.PauseFollowIndexAsync(name, d => d))
				.RequestAsync(c => c.PauseFollowIndexAsync(new PauseFollowIndexRequest(name)));

		}
	}
}
