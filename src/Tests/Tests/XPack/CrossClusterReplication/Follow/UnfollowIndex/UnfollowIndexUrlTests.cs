using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;

namespace Tests.XPack.CrossClusterReplication.Follow.UnfollowIndex
{
	public class UnfollowIndexUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			var name = "x";
			await UrlTester.POST($"/{name}/_ccr/unfollow")
				.Fluent(c => c.UnfollowIndex(name, d => d))
				.Request(c => c.UnfollowIndex(new UnfollowIndexRequest(name)))
				.FluentAsync(c => c.UnfollowIndexAsync(name, d => d))
				.RequestAsync(c => c.UnfollowIndexAsync(new UnfollowIndexRequest(name)));

		}
	}
}
