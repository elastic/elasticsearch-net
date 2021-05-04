// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;

namespace Tests.XPack.Security.ApiKey
{
	public class SecurityApiKeyUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.DELETE("/_security/api_key")
			.Fluent(c => c.Security.InvalidateApiKey(p => p))
			.Request(c => c.Security.InvalidateApiKey(new InvalidateApiKeyRequest()))
			.FluentAsync(c => c.Security.InvalidateApiKeyAsync(p => p))
			.RequestAsync(c => c.Security.InvalidateApiKeyAsync(new InvalidateApiKeyRequest()));
	}

	public class SecurityGetApiKeyUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.GET("/_security/api_key")
			.Fluent(c => c.Security.GetApiKey())
			.Request(c => c.Security.GetApiKey(new GetApiKeyRequest()))
			.FluentAsync(c => c.Security.GetApiKeyAsync())
			.RequestAsync(c => c.Security.GetApiKeyAsync(new GetApiKeyRequest()));
	}

	public class SecurityCreateApiKeyUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await UrlTester.PUT("/_security/api_key")
			.Fluent(c => c.Security.CreateApiKey(p => p))
			.Request(c => c.Security.CreateApiKey(new CreateApiKeyRequest()))
			.FluentAsync(c => c.Security.CreateApiKeyAsync(p => p))
			.RequestAsync(c => c.Security.CreateApiKeyAsync(new CreateApiKeyRequest()));
	}

	public class SecurityClearApiKeyCacheUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await UrlTester.POST("/_security/api_key/id1%2Cid2/_clear_cache")
				.Fluent(c => c.Security.ClearApiKeyCache(f => f.Ids("id1,id2")))
				.Request(c => c.Security.ClearApiKeyCache(new ClearApiKeyCacheRequest("id1,id2")))
				.FluentAsync(c => c.Security.ClearApiKeyCacheAsync(f => f.Ids("id1,id2")))
				.RequestAsync(c => c.Security.ClearApiKeyCacheAsync(new ClearApiKeyCacheRequest("id1,id2")));

			await UrlTester.POST("/_security/api_key/*/_clear_cache")
				.Fluent(c => c.Security.ClearApiKeyCache())
				.Request(c => c.Security.ClearApiKeyCache(new ClearApiKeyCacheRequest()))
				.FluentAsync(c => c.Security.ClearApiKeyCacheAsync())
				.RequestAsync(c => c.Security.ClearApiKeyCacheAsync(new ClearApiKeyCacheRequest()));
		}
	}
}
