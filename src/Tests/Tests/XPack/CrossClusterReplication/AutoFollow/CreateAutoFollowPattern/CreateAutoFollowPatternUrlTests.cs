using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;

namespace Tests.XPack.CrossClusterReplication.AutoFollow.CreateAutoFollowPattern
{
	public class CreateAutoFollowPatternUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			var name = "x";
			await UrlTester.PUT($"/_ccr/auto_follow/{name}")
				.Fluent(c => c.CreateAutoFollowPattern(name, d => d))
				.Request(c => c.CreateAutoFollowPattern(new CreateAutoFollowPatternRequest(name)))
				.FluentAsync(c => c.CreateAutoFollowPatternAsync(name, d => d))
				.RequestAsync(c => c.CreateAutoFollowPatternAsync(new CreateAutoFollowPatternRequest(name)));

		}
	}
}
