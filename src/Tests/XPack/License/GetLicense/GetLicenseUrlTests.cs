using System.Threading.Tasks;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.License.GetLicense
{
	public class GetLicenseUrlTests : IUrlTests
	{
		[U] public async Task Urls()
		{
			await GET("/_license")
				.Fluent(c => c.GetLicense())
				.Request(c => c.GetLicense(new GetLicenseRequest()))
				.FluentAsync(c => c.GetLicenseAsync())
				.RequestAsync(c => c.GetLicenseAsync(new GetLicenseRequest()))
				;
		}
	}
}
