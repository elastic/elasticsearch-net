using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;

namespace Tests.XPack.CrossClusterReplication.Follow.CreateFollowIndex
{
	public class CreateFollowIndexUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			var name = "x";
			await UrlTester.PUT($"/{name}/_ccr/follow")
				.Fluent(c => c.CreateFollowIndex(name, d => d))
				.Request(c => c.CreateFollowIndex(new CreateFollowIndexRequest(name)))
				.FluentAsync(c => c.CreateFollowIndexAsync(name, d => d))
				.RequestAsync(c => c.CreateFollowIndexAsync(new CreateFollowIndexRequest(name)));

		}
	}
}
