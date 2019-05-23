using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.License.GetTrialLicenseStatus
{
	public class GetTrialLicenseStatusUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_license/trial_status")
			.Fluent(c => c.License.GetTrialLicenseStatus())
			.Request(c => c.License.GetTrialLicenseStatus(new GetTrialLicenseStatusRequest()))
			.FluentAsync(c => c.License.GetTrialLicenseStatusAsync())
			.RequestAsync(c => c.License.GetTrialLicenseStatusAsync(new GetTrialLicenseStatusRequest()));
	}
}
