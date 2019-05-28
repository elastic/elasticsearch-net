using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.License.StartBasicLicense
{
	public class StartBasicLicenseUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_license/start_basic")
			.Fluent(c => c.License.StartBasicLicense())
			.Request(c => c.License.StartBasicLicense(new StartBasicLicenseRequest()))
			.FluentAsync(c => c.License.StartBasicLicenseAsync())
			.RequestAsync(c => c.License.StartBasicLicenseAsync(new StartBasicLicenseRequest()));
	}
}
