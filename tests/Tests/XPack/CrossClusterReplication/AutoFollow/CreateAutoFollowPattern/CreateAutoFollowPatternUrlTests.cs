using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.XPack.CrossClusterReplication.AutoFollow.CreateAutoFollowPattern
{
	public class CreateAutoFollowPatternUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			var name = "x";
			await UrlTester.PUT($"/_ccr/auto_follow/{name}")
				.Fluent(c => c.CrossClusterReplication.CreateAutoFollowPattern(name, d => d))
				.Request(c => c.CrossClusterReplication.CreateAutoFollowPattern(new CreateAutoFollowPatternRequest(name)))
				.FluentAsync(c => c.CrossClusterReplication.CreateAutoFollowPatternAsync(name, d => d))
				.RequestAsync(c => c.CrossClusterReplication.CreateAutoFollowPatternAsync(new CreateAutoFollowPatternRequest(name)));

		}
	}
}
