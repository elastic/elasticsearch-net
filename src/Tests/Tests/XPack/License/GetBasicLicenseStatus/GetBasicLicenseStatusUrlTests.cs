using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.License.GetBasicLicenseStatus
{
	public class GetBasicLicenseStatusUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_license/basic_status")
			.Fluent(c => c.License.GetBasicStatus())
			.Request(c => c.License.GetBasicStatus(new GetBasicLicenseStatusRequest()))
			.FluentAsync(c => c.License.GetBasicStatusAsync())
			.RequestAsync(c => c.License.GetBasicStatusAsync(new GetBasicLicenseStatusRequest()));
	}
}
