using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.XPack.CrossClusterReplication.Follow.ForgetFollowerIndex
{
	public class ForgetFollowerIndexUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			var name = "x";
			await UrlTester.POST($"/{name}/_ccr/forget_follower")
				.Fluent(c => c.CrossClusterReplication.ForgetFollowerIndex(name, d => d))
				.Request(c => c.CrossClusterReplication.ForgetFollowerIndex(new ForgetFollowerIndexRequest(name)))
				.FluentAsync(c => c.CrossClusterReplication.ForgetFollowerIndexAsync(name, d => d))
				.RequestAsync(c => c.CrossClusterReplication.ForgetFollowerIndexAsync(new ForgetFollowerIndexRequest(name)));

		}
	}
}
