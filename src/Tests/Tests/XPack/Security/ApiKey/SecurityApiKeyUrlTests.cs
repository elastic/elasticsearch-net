using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;

namespace Tests.XPack.Security.ApiKey
{
	public class SecurityApiKeyUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.DELETE("/_security/api_key")
			.Fluent(c => c.InvalidateApiKey())
			.Request(c => c.InvalidateApiKey(new InvalidateApiKeyRequest()))
			.FluentAsync(c => c.InvalidateApiKeyAsync())
			.RequestAsync(c => c.InvalidateApiKeyAsync(new InvalidateApiKeyRequest()));
	}

	public class SecurityGetApiKeyUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.GET("/_security/api_key")
			.Fluent(c => c.GetApiKey())
			.Request(c => c.GetApiKey(new GetApiKeyRequest()))
			.FluentAsync(c => c.GetApiKeyAsync())
			.RequestAsync(c => c.GetApiKeyAsync(new GetApiKeyRequest()));
	}

	public class SecurityCreateApiKeyUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.PUT("/_security/api_key")
			.Fluent(c => c.CreateApiKey())
			.Request(c => c.CreateApiKey(new CreateApiKeyRequest()))
			.FluentAsync(c => c.CreateApiKeyAsync())
			.RequestAsync(c => c.CreateApiKeyAsync(new CreateApiKeyRequest()));
	}
}
