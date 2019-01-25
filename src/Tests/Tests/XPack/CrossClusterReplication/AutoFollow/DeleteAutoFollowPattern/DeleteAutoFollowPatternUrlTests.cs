using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;

namespace Tests.XPack.CrossClusterReplication.AutoFollow.DeleteAutoFollowPattern
{
	public class DeleteAutoFollowPatternUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			var name = "x";
			await UrlTester.DELETE($"/_ccr/auto_follow/{name}")
				.Fluent(c => c.DeleteAutoFollowPattern(name, d => d))
				.Request(c => c.DeleteAutoFollowPattern(new DeleteAutoFollowPatternRequest(name)))
				.FluentAsync(c => c.DeleteAutoFollowPatternAsync(name, d => d))
				.RequestAsync(c => c.DeleteAutoFollowPatternAsync(new DeleteAutoFollowPatternRequest(name)));

		}
	}
}
