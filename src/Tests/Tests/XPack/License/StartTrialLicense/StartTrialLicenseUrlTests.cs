using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.License.StartTrialLicense
{
	public class StartTrialLicenseUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_license/start_trial")
			.Fluent(c => c.License.StartTrial())
			.Request(c => c.License.StartTrial(new StartTrialLicenseRequest()))
			.FluentAsync(c => c.License.StartTrialAsync())
			.RequestAsync(c => c.License.StartTrialAsync(new StartTrialLicenseRequest()));
	}
}
