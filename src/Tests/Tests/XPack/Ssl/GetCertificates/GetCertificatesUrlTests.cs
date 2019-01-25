using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;

namespace Tests.XPack.Ssl.GetCertificates
{
	public class GetCertificatesUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.GET("/_xpack/ssl/certificates")
			.Fluent(c => c.GetCertificates())
			.Request(c => c.GetCertificates(new GetCertificatesRequest()))
			.FluentAsync(c => c.GetCertificatesAsync())
			.RequestAsync(c => c.GetCertificatesAsync(new GetCertificatesRequest()));
	}
}
