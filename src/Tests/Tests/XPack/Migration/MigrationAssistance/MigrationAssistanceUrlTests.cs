using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.Migration.MigrationAssistance
{
	public class MigrationAssistanceUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_migration/assistance")
					.Fluent(c => c.Migration.Assistance())
					.Request(c => c.Migration.Assistance(new MigrationAssistanceRequest()))
					.FluentAsync(c => c.Migration.AssistanceAsync())
					.RequestAsync(c => c.Migration.AssistanceAsync(new MigrationAssistanceRequest()))
				;

			var index = "another-index";

			await GET($"/_migration/assistance/{index}")
					.Fluent(c => c.Migration.Assistance(index))
					.Request(c => c.Migration.Assistance(new MigrationAssistanceRequest(index)))
					.FluentAsync(c => c.Migration.AssistanceAsync(index))
					.RequestAsync(c => c.Migration.AssistanceAsync(new MigrationAssistanceRequest(index)))
				;
		}
	}
}
