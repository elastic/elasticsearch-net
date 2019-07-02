using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.XPack.CrossClusterReplication.Follow.UnfollowIndex
{
	public class UnfollowIndexUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			var name = "x";
			await UrlTester.POST($"/{name}/_ccr/unfollow")
				.Fluent(c => c.CrossClusterReplication.UnfollowIndex(name, d => d))
				.Request(c => c.CrossClusterReplication.UnfollowIndex(new UnfollowIndexRequest(name)))
				.FluentAsync(c => c.CrossClusterReplication.UnfollowIndexAsync(name, d => d))
				.RequestAsync(c => c.CrossClusterReplication.UnfollowIndexAsync(new UnfollowIndexRequest(name)));

		}
	}
}
