using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.License.GetLicense
{
	public class GetLicenseUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_license")
			.Fluent(c => c.License.Get())
			.Request(c => c.License.Get(new GetLicenseRequest()))
			.FluentAsync(c => c.License.GetAsync())
			.RequestAsync(c => c.License.GetAsync(new GetLicenseRequest()));
	}
}
