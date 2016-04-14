using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.License.DeleteLicense
{
	public class DeleteLicenseUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await DELETE("/_license")
				.Fluent(c => c.DeleteLicense())
				.Request(c => c.DeleteLicense(new DeleteLicenseRequest()))
				.FluentAsync(c => c.DeleteLicenseAsync())
				.RequestAsync(c => c.DeleteLicenseAsync(new DeleteLicenseRequest()))
				;
		}
	}
}
