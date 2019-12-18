using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.XPack.CrossClusterReplication.AutoFollow.ResumeAutoFollowPattern
{
	public class ResumeAutoFollowPatternUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			var name = "x";
			await UrlTester.POST($"/_ccr/auto_follow/{name}/resume")
				.Fluent(c => c.CrossClusterReplication.ResumeAutoFollowPattern(name))
				.Request(c => c.CrossClusterReplication.ResumeAutoFollowPattern(new ResumeAutoFollowPatternRequest(name)))
				.FluentAsync(c => c.CrossClusterReplication.ResumeAutoFollowPatternAsync(name))
				.RequestAsync(c => c.CrossClusterReplication.ResumeAutoFollowPatternAsync(new ResumeAutoFollowPatternRequest(name)));
		}
	}
}
