using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Migration.MigrationUpgrade
{
	public class MigrationUpgradeUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST($"/_xpack/migration/upgrade/index")
				.Fluent(c => c.MigrationUpgrade("index"))
				.Request(c => c.MigrationUpgrade(new MigrationUpgradeRequest("index")))
				.FluentAsync(c => c.MigrationUpgradeAsync("index"))
				.RequestAsync(c => c.MigrationUpgradeAsync(new MigrationUpgradeRequest("index")))
				;
		}
	}
}
