using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;

namespace Tests.XPack.Security.User.SecurityInvalidateApiKey
{
	public class SecurityApiKeyUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.DELETE("/_security/api_key")
			.Fluent(c => c.SecurityInvalidateApiKey())
			.Request(c => c.SecurityInvalidateApiKey(new SecurityInvalidateApiKeyRequest()))
			.FluentAsync(c => c.SecurityInvalidateApiKeyAsync())
			.RequestAsync(c => c.SecurityInvalidateApiKeyAsync(new SecurityInvalidateApiKeyRequest()));
	}

	public class SecurityGetApiKeyUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.GET("/_security/api_key")
			.Fluent(c => c.SecurityGetApiKey())
			.Request(c => c.SecurityGetApiKey(new SecurityGetApiKeyRequest()))
			.FluentAsync(c => c.SecurityGetApiKeyAsync())
			.RequestAsync(c => c.SecurityGetApiKeyAsync(new SecurityGetApiKeyRequest()));
	}

	public class SecurityCreateApiKeyUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.POST("/_security/api_key")
			.Fluent(c => c.SecurityCreateApiKey())
			.Request(c => c.SecurityCreateApiKey(new SecurityCreateApiKeyRequest()))
			.FluentAsync(c => c.SecurityCreateApiKeyAsync())
			.RequestAsync(c => c.SecurityCreateApiKeyAsync(new SecurityCreateApiKeyRequest()));
	}
}
