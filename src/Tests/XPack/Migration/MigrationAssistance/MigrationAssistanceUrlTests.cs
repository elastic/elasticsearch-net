using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Migration.MigrationAssistance
{
	public class MigrationAssistanceUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_xpack/migration/assistance")
				.Fluent(c => c.MigrationAssistance())
				.Request(c => c.MigrationAssistance(new MigrationAssistanceRequest()))
				.FluentAsync(c => c.MigrationAssistanceAsync())
				.RequestAsync(c => c.MigrationAssistanceAsync(new MigrationAssistanceRequest()))
				;

			var index = "another-index";

			await GET($"/_xpack/migration/assistance/{index}")
				.Fluent(c => c.MigrationAssistance(d=>d.Index(index)))
				.Request(c => c.MigrationAssistance(new MigrationAssistanceRequest(index)))
				.FluentAsync(c => c.MigrationAssistanceAsync(d=>d.Index(index)))
				.RequestAsync(c => c.MigrationAssistanceAsync(new MigrationAssistanceRequest(index)))
				;
		}
	}
}
