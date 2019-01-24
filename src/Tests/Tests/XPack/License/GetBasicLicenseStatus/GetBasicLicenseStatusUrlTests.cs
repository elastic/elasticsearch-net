using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.License.GetBasicLicenseStatus
{
	public class GetBasicLicenseStatusUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_xpack/license/basic_status")
			.Fluent(c => c.GetBasicLicenseStatus())
			.Request(c => c.GetBasicLicenseStatus(new GetBasicLicenseStatusRequest()))
			.FluentAsync(c => c.GetBasicLicenseStatusAsync())
			.RequestAsync(c => c.GetBasicLicenseStatusAsync(new GetBasicLicenseStatusRequest()));
	}
}
