using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.License.StartTrialLicense
{
	public class StartTrialLicenseUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await POST("/_xpack/license/start_trial")
				.Fluent(c => c.StartTrialLicense())
				.Request(c => c.StartTrialLicense(new StartTrialLicenseRequest()))
				.FluentAsync(c => c.StartTrialLicenseAsync())
				.RequestAsync(c => c.StartTrialLicenseAsync(new StartTrialLicenseRequest()))
				;
		}
	}
}
