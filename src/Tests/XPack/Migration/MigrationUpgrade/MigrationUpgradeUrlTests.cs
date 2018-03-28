using System.Threading.Tasks;
using Nest;
using Tests.Framework;

namespace Tests.XPack.Migration.MigrationUpgrade
{
	public class MigrationUpgradeUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await UrlTester.GET("/_xpack/migration/assistance")
				.Fluent(c => c.MigrationAssistance())
				.Request(c => c.MigrationAssistance(new MigrationAssistanceRequest()))
				.FluentAsync(c => c.MigrationAssistanceAsync())
				.RequestAsync(c => c.MigrationAssistanceAsync(new MigrationAssistanceRequest()))
				;

			var index = "another-index";

			await UrlTester.GET($"/_xpack/migration/assistance/{index}")
				.Fluent(c => c.MigrationAssistance(d=>d.Index(index)))
				.Request(c => c.MigrationAssistance(new MigrationAssistanceRequest(index)))
				.FluentAsync(c => c.MigrationAssistanceAsync(d=>d.Index(index)))
				.RequestAsync(c => c.MigrationAssistanceAsync(new MigrationAssistanceRequest(index)))
				;
		}
	}
}
