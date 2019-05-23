using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Migration.MigrationUpgrade
{
	public class MigrationUpgradeUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST($"/_migration/upgrade/index")
			.Fluent(c => c.Migration.Upgrade("index"))
			.Request(c => c.Migration.Upgrade(new MigrationUpgradeRequest("index")))
			.FluentAsync(c => c.Migration.UpgradeAsync("index"))
			.RequestAsync(c => c.Migration.UpgradeAsync(new MigrationUpgradeRequest("index")));
	}
}
