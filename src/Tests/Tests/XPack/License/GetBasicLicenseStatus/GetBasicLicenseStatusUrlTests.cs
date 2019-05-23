using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.License.GetBasicLicenseStatus
{
	public class GetBasicLicenseStatusUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_license/basic_status")
			.Fluent(c => c.License.GetBasicLicenseStatus())
			.Request(c => c.License.GetBasicLicenseStatus(new GetBasicLicenseStatusRequest()))
			.FluentAsync(c => c.License.GetBasicLicenseStatusAsync())
			.RequestAsync(c => c.License.GetBasicLicenseStatusAsync(new GetBasicLicenseStatusRequest()));
	}
}
