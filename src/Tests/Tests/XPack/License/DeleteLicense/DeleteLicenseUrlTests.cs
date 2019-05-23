using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.License.DeleteLicense
{
	public class DeleteLicenseUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await DELETE("/_license")
			.Fluent(c => c.License.DeleteLicense())
			.Request(c => c.License.DeleteLicense(new DeleteLicenseRequest()))
			.FluentAsync(c => c.License.DeleteLicenseAsync())
			.RequestAsync(c => c.License.DeleteLicenseAsync(new DeleteLicenseRequest()));
	}
}
