using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.XPack.Ssl.GetCertificates
{
	public class GetCertificatesUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.GET("/_ssl/certificates")
			.Fluent(c => c.Security.GetCertificates())
			.Request(c => c.Security.GetCertificates(new GetCertificatesRequest()))
			.FluentAsync(c => c.Security.GetCertificatesAsync())
			.RequestAsync(c => c.Security.GetCertificatesAsync(new GetCertificatesRequest()));
	}
}
