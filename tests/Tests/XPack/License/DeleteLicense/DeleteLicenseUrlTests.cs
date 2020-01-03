using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.License.DeleteLicense
{
	public class DeleteLicenseUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await DELETE("/_license")
			.Fluent(c => c.License.Delete())
			.Request(c => c.License.Delete(new DeleteLicenseRequest()))
			.FluentAsync(c => c.License.DeleteAsync())
			.RequestAsync(c => c.License.DeleteAsync(new DeleteLicenseRequest()));
	}
}
