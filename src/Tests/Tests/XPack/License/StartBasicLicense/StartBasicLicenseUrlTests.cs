using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.License.StartBasicLicense
{
	public class StartBasicLicenseUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await POST("/_license/start_basic")
			.Fluent(c => c.License.StartBasic())
			.Request(c => c.License.StartBasic(new StartBasicLicenseRequest()))
			.FluentAsync(c => c.License.StartBasicAsync())
			.RequestAsync(c => c.License.StartBasicAsync(new StartBasicLicenseRequest()));
	}
}
