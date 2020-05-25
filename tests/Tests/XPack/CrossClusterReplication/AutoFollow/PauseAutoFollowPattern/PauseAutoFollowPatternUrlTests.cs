using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.XPack.CrossClusterReplication.AutoFollow.PauseAutoFollowPattern
{
	public class PauseAutoFollowPatternUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			var name = "x";
			await UrlTester.POST($"/_ccr/auto_follow/{name}/pause")
				.Fluent(c => c.CrossClusterReplication.PauseAutoFollowPattern(name))
				.Request(c => c.CrossClusterReplication.PauseAutoFollowPattern(new PauseAutoFollowPatternRequest(name)))
				.FluentAsync(c => c.CrossClusterReplication.PauseAutoFollowPatternAsync(name))
				.RequestAsync(c => c.CrossClusterReplication.PauseAutoFollowPatternAsync(new PauseAutoFollowPatternRequest(name)));
		}
	}
}
