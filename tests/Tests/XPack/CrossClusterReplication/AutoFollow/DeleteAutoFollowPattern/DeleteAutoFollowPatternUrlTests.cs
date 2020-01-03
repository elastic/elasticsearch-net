using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.XPack.CrossClusterReplication.AutoFollow.DeleteAutoFollowPattern
{
	public class DeleteAutoFollowPatternUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			var name = "x";
			await UrlTester.DELETE($"/_ccr/auto_follow/{name}")
				.Fluent(c => c.CrossClusterReplication.DeleteAutoFollowPattern(name, d => d))
				.Request(c => c.CrossClusterReplication.DeleteAutoFollowPattern(new DeleteAutoFollowPatternRequest(name)))
				.FluentAsync(c => c.CrossClusterReplication.DeleteAutoFollowPatternAsync(name, d => d))
				.RequestAsync(c => c.CrossClusterReplication.DeleteAutoFollowPatternAsync(new DeleteAutoFollowPatternRequest(name)));

		}
	}
}
