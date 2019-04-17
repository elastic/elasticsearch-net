using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;

namespace Tests.XPack.Security.User.SecurityInvalidateApiKey
{
	public class SecurityInvalidateApiKeyUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.DELETE("/_xpack/security/oauth2/token")
			.Fluent(c => c.SecurityInvalidateApiKey())
			.Request(c => c.SecurityInvalidateApiKey(new SecurityInvalidateApiKeyRequest()))
			.FluentAsync(c => c.SecurityInvalidateApiKeyAsync())
			.RequestAsync(c => c.SecurityInvalidateApiKeyAsync(new SecurityInvalidateApiKeyRequest()));
	}
}
