using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.License.GetTrialLicenseStatus
{
	public class GetTrialLicenseStatusUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_xpack/license/trial_status")
				.Fluent(c => c.GetTrialLicenseStatus())
				.Request(c => c.GetTrialLicenseStatus(new GetTrialLicenseStatusRequest()))
				.FluentAsync(c => c.GetTrialLicenseStatusAsync())
				.RequestAsync(c => c.GetTrialLicenseStatusAsync(new GetTrialLicenseStatusRequest()))
				;
		}
	}
}
